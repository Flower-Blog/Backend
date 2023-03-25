using DotnetWebApi.Dto;
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
        public UserController(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }
        // TODO: 創建使用者

        [HttpPost]
        public ActionResult CreateUser([FromBody] string Address)
        {
            var result = _dbContext.Users.Find(Address);
            return Ok();
        }
    }
}


