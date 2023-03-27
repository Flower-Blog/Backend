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
        [ProducesResponseType(typeof(RegisterDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterDto404), StatusCodes.Status404NotFound)]
        public ActionResult Register(RegisterDto value)
        {
            var userETF = (from x in _dbContext.Users
                           where x.Email == value.Email || x.Name == value.Name || x.Address == value.Address
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

                return Ok(new
                {
                    StatusCode = 404,
                    Title = "拒絕成功"
                });
                // FIXME: 記得改成201狀態碼
                // return new ObjectResult(entity) { StatusCode = StatusCodes.Status201Created };
            }
            // FIXME: 記得改成403狀態碼
            return NotFound(new
            {
                StatusCode = 404,
                Title = "拒絕註冊"
            });
        }

        /// <summary>
        /// 確認電子郵件是否使用過
        /// </summary>
        /// <param name="email" example="andy@gmail.com">會員Email</param>
        /// <returns></returns>
        [HttpGet("/user/register/email/{email}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegisterEmailDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterEmailDto404), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RegisterEmail(string email)
        {
            var userETF = (from x in _dbContext.Users
                           where x.Email == email
                           select x).FirstOrDefault();

            var mailETF = (from x in _dbContext.Mail
                           where x.Email == email
                           select x).FirstOrDefault();

            if (userETF == null)
            {
                var VerificationCode = Guid.NewGuid().ToString();
                // 找不到使用者email 就幫他新增進去
                if (mailETF == null)
                {
                    _dbContext.Mail.Add(new Mail
                    {
                        Email = email,
                        VerificationCode = VerificationCode
                    });
                    _dbContext.SaveChanges();
                }
                // 找到使用者email 就幫他更新一組亂數
                else
                {
                    mailETF.VerificationCode = VerificationCode;
                    _dbContext.Entry(mailETF).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }


                try
                {
                    MailRequest request = new MailRequest();
                    request.ToEmail = email;
                    request.Subject = "Floor Blog 電子郵件驗證";
                    request.Body = $"<h1>驗證碼為:</h1><p>{VerificationCode}</p>";
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
        /// <param name="name" example="Andy">會員Email</param>
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