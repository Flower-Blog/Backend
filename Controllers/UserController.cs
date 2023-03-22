using DotnetWebApi.Models;
using Microsoft.AspNetCore.Mvc;


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

         [HttpPost]
         public ActionResult CreateUser(){
            return Ok();
        }
    }
}