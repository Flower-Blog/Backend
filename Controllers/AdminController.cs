using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using DotnetWebApi.Dto;
using DotnetWebApi.Helper;
using DotnetWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly BlogContext _dbContext;
        public AdminController(BlogContext dbContext, IMailService mailService)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 使用者紀錄(最新)
        /// </summary>
        [HttpGet("/admin/user")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(GetNewUsersDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetNewUsersDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult GetNewUsers()
        {
            try
            {
                // 拿取所有文章
                var users = _dbContext.Users
                                         .Select(a => new GetNewUsersDto
                                         {
                                             Name = a.Name,
                                             Address = a.Address,
                                             Email = a.Email,
                                             Introduction = a.Introduction,
                                             Picture = a.Picture,
                                             CreatedAt = a.CreatedAt,
                                         })
                                         .OrderByDescending(a => a.CreatedAt)
                                         .Take(10)
                                         .ToList();

                return Ok(new { StatusCode = 200, users });
            }
            catch
            {
                return StatusCode(500, "取得使用者紀錄失敗");
            }
        }

        /// <summary>
        /// 花總類
        /// </summary>
        [HttpGet("/admin/flowers")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(GetAllFlowersDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAllFlowersDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult GetAllFlowers()
        {
            try
            {
                // 拿取所有文章
                var flowers = _dbContext.Flowers
                                            .Select(a => new GetAllFlowersDto
                                            {
                                                Id = a.Id,
                                                Name = a.Name,
                                                Language = a.Language
                                            }).ToList();

                return Ok(new { StatusCode = 200, flowers });
            }
            catch
            {
                return StatusCode(500, "取得花總類失敗");
            }
        }
    }
}