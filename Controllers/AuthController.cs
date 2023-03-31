using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using DotnetWebApi.Dto;
using DotnetWebApi.Helper;
using DotnetWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Web3;

namespace DotnetWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly BlogContext _dbContext;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public AuthController(BlogContext dbContext, IMailService mailService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mailService = mailService;
            _configuration = configuration;
        }

        /// <summary>
        /// 登入拿TOKEN
        /// </summary>
        [HttpPost("/auth/login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(LoginDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(LoginDto401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(LoginDto404), StatusCodes.Status404NotFound)]
        public ActionResult Login(LoginDto value)
        {
            var userETF = (from x in _dbContext.Users
                           where x.Address == value.address
                           select x).FirstOrDefault();
            if (userETF == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Title = "找不到該使用者"
                });
            }
            else if (VerifySignature(userETF.Nonce, value.address, value.signature))
            {
                var token = jwtLogin(userETF.Name, userETF.Email, userETF.Address, _configuration["JWT:KEY"], _configuration["JWT:Issuer"], _configuration["JWT:Audience"]);
                return Ok(new
                {
                    StatusCode = 200,
                    token = token
                });
            }

            return Unauthorized(new
            {
                StatusCode = 401,
                Title = "登入驗證失敗"
            });
        }

        // TODO: 驗證簽證
        private static bool VerifySignature(string nonce, string address, string signature)
        {
            try
            {
                var signer = new EthereumMessageSigner();
                var signerAddress = signer.EncodeUTF8AndEcRecover(nonce, signature);
                if (signerAddress.ToLower() != address.ToLower())
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // TODO: 設定TOKEN
        private static string jwtLogin(string name, string email, string address, string key, string issuer, string audience)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, name),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim("address", address)
                };
            //設定Role
            // foreach (var temp in role)
            // {
            //     claims.Add(new Claim(ClaimTypes.Role, temp.Name));
            // }
            //取出appsettings.json裡的KEY處理
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            //設定jwt相關資訊
            var jwt = new JwtSecurityToken
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );


            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }

        /// <summary>
        /// 確認是否是會員
        /// </summary>
        /// <param name="address" example="0x34B605B3d13923a60a629794C15B103C44beaE1c">會員錢包地址</param>
        /// <returns></returns>
        [HttpGet("/auth/login/{address}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IsUserDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IsUserDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IsUserDto404), StatusCodes.Status404NotFound)]
        public ActionResult IsUser(string address)
        {
            if (address.Length != 42)
            {
                return BadRequest(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "One or more validation errors occurred.",
                    status = 400,
                    errors = new { address = new[] { "address必須等於42個字元" } }
                });
            }
            var userETF = (from x in _dbContext.Users
                           where x.Address == address
                           select x).FirstOrDefault();

            if (userETF != null)
            {
                var guid = Guid.NewGuid().ToString();
                userETF.Nonce = guid;
                _dbContext.Entry(userETF).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return Ok(new
                {
                    StatusCode = 200,
                    nonce = guid
                });
            }
            return NotFound(new
            {
                StatusCode = 404,
                Title = "找不到該使用者"
            });
        }

        /// <summary>
        /// 確認電子郵件驗證碼是否正確
        /// </summary>
        /// <param name="Email" example="andy910812@gmail.com">信箱</param>
        /// <param name="VerificationCode" example="684414">信箱驗證碼</param>
        /// <returns></returns>
        [HttpGet("/auth/register/{Email}/{VerificationCode}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ConfirmationEMailDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ConfirmationEMailDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ConfirmationEMailDto401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ConfirmationEMailDto404), StatusCodes.Status404NotFound)]
        public ActionResult ConfirmationEMail(string Email, int VerificationCode)
        {
            if (Regex.IsMatch(Email, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$") == false || VerificationCode < 100000 || VerificationCode > 999999)
            {
                dynamic response = new ExpandoObject();
                response.type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                response.title = "One or more validation errors occurred.";
                response.status = 400;
                dynamic errorsresponse = new ExpandoObject();
                if (!Regex.IsMatch(Email, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")) errorsresponse.email = new[] { "email格式不正確" };
                if (VerificationCode < 100000 || VerificationCode > 999999) errorsresponse.verificationCode = new[] { "驗證碼格式不正確 提示只有6碼" };
                response.errors = errorsresponse;
                return BadRequest(response);
            }
            var MailETF = (from x in _dbContext.Mail
                           where x.Email == Email && x.VerificationCode == VerificationCode
                           select x).FirstOrDefault();

            var IsMail = (from x in _dbContext.Mail
                          where x.Email == Email
                          select x).FirstOrDefault();

            if (MailETF != null)
            {
                var TimeGap = DateTime.Now - MailETF.UpdatedAt;
                var tenMinutes = new TimeSpan(0, 10, 0);
                if (TimeGap > tenMinutes)
                {
                    return Unauthorized(new
                    {
                        StatusCode = 401,
                        Title = "超過10分鐘的驗證時間"
                    });
                }
                MailETF.Verified = true;
                _dbContext.SaveChanges();

                var guid = Guid.NewGuid().ToString();
                var userETF = (from x in _dbContext.Users
                               where x.Name == guid
                               select x).FirstOrDefault();
                // TODO: 不能給使用者重複的名稱 如有重複一直抓UUID直到沒有重複
                while (userETF != null)
                {
                    guid = Guid.NewGuid().ToString();
                    userETF = (from x in _dbContext.Users
                               where x.Name == guid
                               select x).FirstOrDefault();
                }
                return Ok(new
                {
                    StatusCode = 200,
                    name = guid
                });
            }
            else if (IsMail == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    title = "此信箱根本沒有發送過驗證碼歐!"
                });
            }
            return NotFound(new
            {
                StatusCode = 404,
                title = "信箱驗證錯誤"
            });
        }
    }
}