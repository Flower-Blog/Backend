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
        /// 確認電子郵件
        /// </summary>
        /// <param name="email" example="andy@gmail.com">會員Email</param>
        /// <returns></returns>
        [HttpGet("/user/register/{email}")]
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
    }
}