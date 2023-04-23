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
    public class ArticleController : ControllerBase
    {
        private readonly BlogContext _dbContext;
        private readonly IMailService _mailService;
        public ArticleController(BlogContext dbContext, IMailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
        }

        /// <summary>
        /// 取得所有文章(最新)
        /// </summary>
        [HttpGet("/articles")]
        [ProducesResponseType(typeof(GetAllArticlesDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAllArticlesDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult GetAllArticles()
        {
            try
            {
                // 拿取所有文章
                var articles = _dbContext.Articles
                                         .Include(a => a.User)
                                         .Where(a => a.State == true)
                                         .Select(a => new GetAllArticlesDto
                                         {
                                             Id = a.Id,
                                             address = a.User.Address,
                                             Title = a.Title,
                                             SubStandard = a.SubStandard,
                                             Contents = a.Contents,
                                             CreatedAt = a.CreatedAt,
                                             UpdatedAt = a.UpdatedAt
                                         })
                                         .OrderByDescending(a => a.CreatedAt)
                                         .ToList();
                return Ok(new { StatusCode = 200, articles });
            }
            catch
            {
                return StatusCode(500, "取得所有文章失敗");
            }
        }

        /// <summary>
        /// 取得單一文章
        /// </summary>
        /// <param name="id" example="2">文章編號</param>
        /// <returns></returns>
        [HttpGet("/articles/{id}")]
        [ProducesResponseType(typeof(GetArticleDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetArticleDto404), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetArticleDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult GetArticle(int id)
        {
            try
            {
                // 拿取單一文章
                var articles = (_dbContext.Articles
                                         .Include(a => a.User)
                                         .Where(a => a.State == true && a.Id == id)
                                         .Select(a => new GetAllArticlesDto
                                         {
                                             Id = a.Id,
                                             address = a.User.Address,
                                             Title = a.Title,
                                             SubStandard = a.SubStandard,
                                             Contents = a.Contents,
                                             CreatedAt = a.CreatedAt,
                                             UpdatedAt = a.UpdatedAt
                                         })).FirstOrDefault();
                if (articles != null)
                    return Ok(new { StatusCode = 200, articles });
                else
                    return NotFound(new { StatusCode = 404, title = "找不到文章" });
            }
            catch
            {
                return StatusCode(500, "取得所有文章失敗");
            }
        }

        /// <summary>
        /// 新增個人文章
        /// </summary>
        [HttpPost("/articles")]
        [Authorize(Roles = "user,admin")]
        [ProducesResponseType(typeof(AddArticleDto201), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AddArticleDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AddArticleDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult AddArticle([FromBody] AddArticleDto value)
        {
            // 拿取 
            var authHeader = HttpContext.Request.Headers["Authorization"];

            // 從authorization header提取Bearer
            var token = authHeader.ToString().Replace("Bearer ", "");

            // 解碼 token 並取得其聲明
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);

            // 從解碼後的 token 中取得 payload
            var payload = decodedToken.Payload;

            // 從 payload 拿取address
            string userAddress = (string)payload["address"];

            // 從user資料表找user id
            int userId = (from a in _dbContext.Users
                          where a.Address == userAddress
                          select a.Id).FirstOrDefault();

            var newArticle = new Article
            {
                UserId = userId,
                Title = value.Title,
                SubStandard = value.SubStandard,
                Contents = value.Contents,
                State = true
            };
            try
            {
                _dbContext.Articles.Add(newArticle);
                _dbContext.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "新增文章失敗");
            }

            return CreatedAtAction("GetArticle", "Article", new { id = newArticle.Id }, value);
        }

        /// <summary>
        /// 取得個人所有文章(最新)
        /// </summary>
        /// <param name="address" example="2">錢包地址</param>
        /// <returns></returns>
        [HttpGet("/articles/user/{address}")]
        [ProducesResponseType(typeof(GetArticleDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetArticleDto404), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetArticleDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult GetUserArticles(string address)
        {
            try
            {
                // 拿取單一文章
                var articles = (_dbContext.Articles

                                          .Include(a => a.User)
                                          .Where(a => a.State == true && a.User.Address == address)
                                          .Select(a => new GetAllArticlesDto
                                          {
                                              Id = a.Id,
                                              address = a.User.Address,
                                              Title = a.Title,
                                              SubStandard = a.SubStandard,
                                              Contents = a.Contents,
                                              CreatedAt = a.CreatedAt,
                                              UpdatedAt = a.UpdatedAt
                                          }))
                                          .OrderByDescending(a => a.CreatedAt)
                                          .ToList();
                if (articles.Count != 0)
                    return Ok(new { StatusCode = 200, articles });
                else
                    return NotFound(new { StatusCode = 404, title = "使用者沒有文章" });
            }
            catch
            {
                return StatusCode(500, "取得所有文章失敗");
            }
        }
    }
}