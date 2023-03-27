using DotnetWebApi.Dto;
using DotnetWebApi.Helper;
using DotnetWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly BlogContext _dbContext;
        private readonly IMailService _mailService;

        public AuthController(BlogContext dbContext, IMailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
        }

        /// <summary>
        /// 確認是否是會員
        /// </summary>
        /// <param name="address" example="0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089">會員錢包地址</param>
        /// <returns></returns>
        [HttpGet("/auth/register/{address}")]
        public ActionResult IsUser(string address)
        {
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
                Title = "已經註冊過"
            });
        }


    }
}

