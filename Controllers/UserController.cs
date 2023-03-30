using System.Dynamic;
using System.Text.RegularExpressions;
using DotnetWebApi.Dto;
using DotnetWebApi.Helper;
using DotnetWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BlogContext _dbContext;
        private readonly IMailService _mailService;
        public UserController(BlogContext dbContext, IMailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
        }

        /// <summary>
        /// 註冊
        /// </summary>
        [HttpPost("/user/register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegisterDto201), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterDto409), StatusCodes.Status409Conflict)]
        public ActionResult Register(RegisterDto value)
        {
            var userETF = (from x in _dbContext.Users
                           where x.Email == value.Email || x.Name == value.Name || x.Address == value.Address
                           select x).FirstOrDefault();
            var EmailUse = (from x in _dbContext.Users
                            where x.Email == value.Email
                            select x).FirstOrDefault();
            var NameUse = (from x in _dbContext.Users
                           where x.Name == value.Name
                           select x).FirstOrDefault();
            var AddressUse = (from x in _dbContext.Users
                              where x.Address == value.Address
                              select x).FirstOrDefault();

            if (userETF == null)
            {
                var entity = _dbContext.Users.Add(new User
                {
                    Name = value.Name,
                    Address = value.Address,
                    Email = value.Email,
                    Nonce = Guid.NewGuid().ToString(),
                    Admin = false
                });
                _dbContext.SaveChanges();

                return CreatedAtAction("IsUser", "Auth", new { address = value.Address }, value);

            }
            dynamic response = new ExpandoObject();
            response.StatusCode = 409;
            response.Title = "有資料被使用過了 拒絕註冊";
            if (EmailUse != null) response.Email = "信箱被使用過";
            if (NameUse != null) response.Name = "名稱被使用過";
            if (AddressUse != null) response.Address = "地址被使用過";
            return Conflict(response);
        }

        /// <summary>
        /// 確認電子郵件是否使用過
        /// </summary>
        /// <param name="email" example="andy910812@gmail.com">會員Email</param>
        /// <returns></returns>
        [HttpGet("/user/register/email/{email}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegisterEmailDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterEmailDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterEmailDto404), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RegisterEmail(string email)
        {
            if (Regex.IsMatch(email, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$") == false)
            {
                return BadRequest(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "One or more validation errors occurred.",
                    status = 400,
                    errors = new { address = new[] { "email格式不正確" } }
                });
            }
            var userETF = (from x in _dbContext.Users
                           where x.Email == email
                           select x).FirstOrDefault();

            var mailETF = (from x in _dbContext.Mail
                           where x.Email == email
                           select x).FirstOrDefault();
            var test = (from x in _dbContext.Mail
                        select x).Count();

            if (userETF == null)
            {
                Random rd = new Random();
                var VerificationCode = rd.Next(100000, 1000000).ToString();

                // 找不到使用者email 就幫他新增進去
                if (mailETF == null)
                {
                    Console.WriteLine("email: " + email);
                    Console.WriteLine("VerificationCode: " + VerificationCode);
                    var entity = _dbContext.Mail.Add(new Mail
                    {
                        Email = email,
                        VerificationCode = VerificationCode,
                    });
                    _dbContext.SaveChanges();
                }
                // 找到使用者email 就幫他更新一組亂數
                else
                {
                    mailETF.VerificationCode = VerificationCode;
                    mailETF.UpdatedAt = DateTime.Now;
                    _dbContext.Entry(mailETF).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }


                try
                {
                    MailRequest request = new MailRequest();
                    request.ToEmail = email;
                    request.Subject = "Floor Blog 電子郵件驗證";
                    request.Body = _mailService.MailBody(VerificationCode);
                    await _mailService.SendEmailiAsync(request);
                    return Ok(new
                    {
                        StatusCode = 200,
                        Title = "已發送驗證碼到郵件"
                    });
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return NotFound(new
            {
                StatusCode = 404,
                Title = "信箱已被使用過"
            });

        }


        /// <summary>
        /// 確認使用者名稱是否使用過
        /// </summary>
        /// <param name="name" example="Andy">會員名稱</param>
        /// <returns></returns>
        [HttpGet("/user/register/name/{name}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegisterNameDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterNameDto404), StatusCodes.Status404NotFound)]
        public ActionResult RegisterName(string name)
        {
            var userETF = (from x in _dbContext.Users
                           where x.Name == name
                           select x).FirstOrDefault();

            if (userETF == null)
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Title = "名稱未使用過"
                });
            }

            return NotFound(new
            {
                StatusCode = 404,
                Title = "名稱已被使用過"
            });
        }


    }
}