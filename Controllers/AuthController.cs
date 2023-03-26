using DotnetWebApi.Dto;
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
        public AuthController(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 確認是否是會員
        /// </summary>
        /// <param name="address" example="0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089">會員Id</param>
        /// <returns></returns>
        [HttpGet("/auth/login/{address}")]
        public ActionResult CreateUser(string address)
        {
            var user = (from x in _dbContext.Users
                        where x.Address == address
                        select x).FirstOrDefault();

            if (user != null)
            {
                var guid = Guid.NewGuid().ToString();
                user.Nonce = guid;
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return Ok(new
                {
                    StatusCode = 200,
                    nonce = guid
                });
            }

            return NotFound();
        }

    }
}