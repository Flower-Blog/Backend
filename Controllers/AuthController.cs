using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using DotnetWebApi.Dto;
using DotnetWebApi.HandleFunction;
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
            else if (AuthHandle.VerifySignature(userETF.Nonce, value.address, value.signature))
            {
                var token = AuthHandle.jwtLogin(userETF.Name, userETF.Email, userETF.Address, _configuration["JWT:KEY"], _configuration["JWT:Issuer"], _configuration["JWT:Audience"]);
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
        /// <param name="email" example="andy910812@gmail.com">信箱</param>
        /// <param name="verificationCode" example="684414">信箱驗證碼</param>
        /// <returns></returns>
        [HttpGet("/auth/register/{email}/{verificationCode}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ConfirmationEMailDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ConfirmationEMailDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ConfirmationEMailDto401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ConfirmationEMailDto404), StatusCodes.Status404NotFound)]
        public ActionResult ConfirmationEMail(string email, int verificationCode)
        {
            if (Regex.IsMatch(email, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$") == false || verificationCode < 100000 || verificationCode > 999999)
            {
                dynamic response = new ExpandoObject();
                response.type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                response.title = "One or more validation errors occurred.";
                response.status = 400;
                dynamic errorsresponse = new ExpandoObject();
                if (!Regex.IsMatch(email, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")) errorsresponse.email = new[] { "email格式不正確" };
                if (verificationCode < 100000 || verificationCode > 999999) errorsresponse.verificationCode = new[] { "驗證碼格式不正確 提示只有6碼" };
                response.errors = errorsresponse;
                return BadRequest(response);
            }
            var MailETF = (from x in _dbContext.Mail
                           where x.Email == email && x.VerificationCode == verificationCode
                           select x).FirstOrDefault();

            var IsMail = (from x in _dbContext.Mail
                          where x.Email == email
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