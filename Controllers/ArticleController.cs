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
        [HttpGet("/articles/new")]
        [ProducesResponseType(typeof(GetAllNewArticlesDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAllNewArticlesDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult GetAllNewArticles()
        {
            try
            {
                // 拿取所有文章
                var articlesQuery = _dbContext.Articles
                                         .Include(a => a.User)
                                         .Where(a => a.State == true)
                                         .Select(a => new GetAllNewArticlesDto
                                         {
                                             Id = a.Id,
                                             Title = a.Title,
                                             SubStandard = a.SubStandard,
                                             Contents = a.Contents,
                                             FlowerCount = a.FlowerCount,
                                             CreatedAt = a.CreatedAt,
                                             UpdatedAt = a.UpdatedAt,
                                             userdata = new UserDataDto
                                             {
                                                 Name = a.User.Name,
                                                 Picture = a.User.Picture
                                             },
                                         })
                                         .OrderByDescending(a => a.CreatedAt)
                                         .Take(10);
                var articles = articlesQuery.Any() ? articlesQuery.ToList() : new List<GetAllNewArticlesDto>();

                return Ok(new { StatusCode = 200, articles });
            }
            catch
            {
                return StatusCode(500, "取得所有文章失敗");
            }
        }
        /// <summary>
        /// 取得所有文章(最熱門)
        /// </summary>
        [HttpGet("/articles/hot")]
        [ProducesResponseType(typeof(GetAllNewArticlesDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAllNewArticlesDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult GetAllHotArticles()
        {
            try
            {
                // 拿取所有文章
                var articlesQuery = _dbContext.Articles
                                         .Include(a => a.User)
                                         .Where(a => a.State == true)
                                         .Select(a => new GetAllNewArticlesDto
                                         {
                                             Id = a.Id,
                                             Title = a.Title,
                                             SubStandard = a.SubStandard,
                                             Contents = a.Contents,
                                             FlowerCount = a.FlowerCount,
                                             CreatedAt = a.CreatedAt,
                                             UpdatedAt = a.UpdatedAt,
                                             userdata = new UserDataDto
                                             {
                                                 Name = a.User.Name,
                                                 Picture = a.User.Picture
                                             },
                                         })
                                         .OrderByDescending(a => a.FlowerCount)
                                         .ThenByDescending(a => a.CreatedAt)
                                         .Take(10);

                var articles = articlesQuery.Any() ? articlesQuery.ToList() : new List<GetAllNewArticlesDto>();

                return Ok(new { StatusCode = 200, articles });
            }
            catch
            {
                return StatusCode(500, "取得所有文章失敗");
            }
        }

        /// <summary>
        /// 取得個人所有文章(最新)
        /// </summary>
        /// <param name="address" example="0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089">錢包地址</param>
        /// <returns></returns>
        [HttpGet("/articles/user/{address}/new")]
        [ProducesResponseType(typeof(GetUserNewArticlesDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetUserNewArticlesDto404), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetUserNewArticlesDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult GetUserNewArticles(string address)
        {
            try
            {
                // 拿取單一文章
                var articlesQuery = (_dbContext.Articles
                                          .Include(a => a.User)
                                          .Where(a => a.State == true && a.User.Address == address)
                                          .Select(a => new GetUserNewArticlesDto
                                          {
                                              Id = a.Id,
                                              Title = a.Title,
                                              SubStandard = a.SubStandard,
                                              Contents = a.Contents,
                                              FlowerCount = a.FlowerCount,
                                              CreatedAt = a.CreatedAt,
                                              UpdatedAt = a.UpdatedAt
                                          }))
                                          .OrderByDescending(a => a.CreatedAt)
                                          .Take(10);
                var articles = articlesQuery.Any() ? articlesQuery.ToList() : new List<GetUserNewArticlesDto>();

                if (articlesQuery.Any())
                    return Ok(new { StatusCode = 200, articles });
                else
                    return NotFound(new { StatusCode = 404, title = "使用者沒有文章" });
            }
            catch
            {
                return StatusCode(500, "取得個人所有文章");
            }
        }

        /// <summary>
        /// 取得個人所有文章(最熱門)
        /// </summary>
        /// <param name="address" example="0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089">錢包地址</param>
        /// <returns></returns>
        [HttpGet("/articles/user/{address}/hot")]
        [ProducesResponseType(typeof(GetUserNewArticlesDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetUserNewArticlesDto404), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetUserNewArticlesDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult GetUserHotArticles(string address)
        {
            try
            {
                // 拿取單一文章
                var articlesQuery = (_dbContext.Articles
                                          .Include(a => a.User)
                                          .Where(a => a.State == true && a.User.Address == address)
                                          .Select(a => new GetUserNewArticlesDto
                                          {
                                              Id = a.Id,
                                              Title = a.Title,
                                              SubStandard = a.SubStandard,
                                              Contents = a.Contents,
                                              FlowerCount = a.FlowerCount,
                                              CreatedAt = a.CreatedAt,
                                              UpdatedAt = a.UpdatedAt
                                          }))
                                         .OrderByDescending(a => a.FlowerCount)
                                         .ThenByDescending(a => a.CreatedAt)
                                         .Take(10);
                var articles = articlesQuery.Any() ? articlesQuery.ToList() : new List<GetUserNewArticlesDto>();

                if (articlesQuery.Any())
                    return Ok(new { StatusCode = 200, articles });
                else
                    return NotFound(new { StatusCode = 404, title = "使用者沒有文章" });
            }
            catch
            {
                return StatusCode(500, "取得個人所有文章");
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
                var comments = _dbContext.Articles
                                         .Include(a => a.User)
                                         .Where(a => a.Id == id)
                                         .SelectMany(a => a.Comments)
                                         .OrderByDescending(a => a.CreatedAt)
                                         .Take(10)
                                         .Select(a => new CommentsDto
                                         {
                                             Id = a.Id,
                                             Contents = a.Contents,
                                             Likes = a.Likes,
                                             CreatedAt = a.CreatedAt,
                                             userdata = new UserDataDto
                                             {
                                                 Name = a.User.Name,
                                                 Picture = a.User.Picture
                                             },
                                         });

                // 拿取單一文章
                var article = (_dbContext.Articles
                                         .Include(a => a.User)
                                         .Where(a => a.State == true && a.Id == id)
                                         .Select(a => new GetArticleDto
                                         {
                                             Id = a.Id,
                                             Title = a.Title,
                                             SubStandard = a.SubStandard,
                                             Contents = a.Contents,
                                             CreatedAt = a.CreatedAt,
                                             UpdatedAt = a.UpdatedAt,
                                             FlowerCount = a.FlowerCount,
                                             userdata = new UserDataDto
                                             {
                                                 Name = a.User.Name,
                                                 Picture = a.User.Picture
                                             },
                                             Comments = comments.ToArray()
                                         })).FirstOrDefault();
                if (article != null)
                    return Ok(new { StatusCode = 200, article });
                else
                    return NotFound(new { StatusCode = 404, title = "找不到文章" });
            }
            catch
            {
                return StatusCode(500, "取得單一文章");
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
        /// 編輯個人文章
        /// </summary>
        /// <param name="id" example="2">文章編號</param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPatch("/articles/{id}")]
        [Authorize(Roles = "user,admin")]
        [ProducesResponseType(typeof(EditArticleDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EditArticleDto401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(EditArticleDto403), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(EditArticleDto404), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EditArticleDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult EditArticle(int id, [FromBody] EditArticleDto value)
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

            var article = _dbContext.Articles.SingleOrDefault(u => u.Id == id);
            if (article == null)
                return NotFound(new { StatusCode = 404, title = "找不到文章" });
            var user = _dbContext.Users.SingleOrDefault(u => u.Address == userAddress);

            if (article.UserId != user.Id)
                return new ObjectResult(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "你不是這邊文章的作者",
                    status = 403,
                })
                {
                    StatusCode = 403
                };

            article.Title = value.Title;
            article.SubStandard = value.SubStandard;
            article.Contents = value.Contents;
            article.UpdatedAt = DateTime.Now;
            try
            {
                // 樂觀併發:
                _dbContext.Entry(article).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return Ok(new
                {
                    StatusCode = 200,
                    title = "修改成功"
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                // 若同步修改失敗
                if (!_dbContext.Users.Any(u => u.Address == userAddress))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        title = "同步修改失敗"
                    });
                }
                else
                {
                    return StatusCode(500, "存取發生錯誤");
                }
            }
        }

        /// <summary>
        /// 刪除個人文章
        /// </summary>
        /// <param name="id" example="2">文章編號</param>
        /// <returns></returns>
        [HttpDelete("/articles/{id}")]
        [Authorize(Roles = "user,admin")]
        [ProducesResponseType(typeof(DeleteArticleDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteArticleDto401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(DeleteArticleDto403), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(DeleteArticleDto404), StatusCodes.Status404NotFound)]
        public IActionResult DeleteArticle(int id)
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

            var user = _dbContext.Users.SingleOrDefault(u => u.Address == userAddress);

            var article = _dbContext.Articles.SingleOrDefault(u => u.Id == id);
            if (article == null)
                return NotFound(new { StatusCode = 404, title = "找不到文章" });

            if (article.UserId != user.Id)
                return new ObjectResult(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "你不是這邊文章的作者",
                    status = 403,
                })
                {
                    StatusCode = 403
                };

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // 找出指定文章的所有留言
                    var comments = _dbContext.Comments.Where(c => c.ArticleId == id).ToList();

                    // 刪除留言的按讚紀錄
                    foreach (var comment in comments)
                    {
                        var commentLikes = _dbContext.CommentLikes.Where(cl => cl.CommentId == comment.Id);
                        _dbContext.CommentLikes.RemoveRange(commentLikes);
                        _dbContext.SaveChanges();
                    }

                    // 刪除留言
                    _dbContext.Comments.RemoveRange(comments);
                    _dbContext.SaveChanges();

                    // 刪除文章送花紀錄
                    var flowerGivers = _dbContext.FlowerGivers.Where(fg => fg.ArticleId == id);
                    _dbContext.FlowerGivers.RemoveRange(flowerGivers);

                    _dbContext.SaveChanges();

                    // 刪除文章
                    _dbContext.Articles.Remove(article);
                    _dbContext.SaveChanges();


                    transaction.Commit();
                    return Ok(new
                    {
                        StatusCode = 200,
                        title = "刪除成功"
                    });
                }
                catch (Exception)
                {
                    transaction.Rollback(); // 發生錯誤時回滾交易
                    throw; // 繼續拋出例外
                }
            }
        }

        /// <summary>
        /// 送花
        /// </summary>
        [HttpPost("/articles/flower")]
        [Authorize(Roles = "user,admin")]
        [ProducesResponseType(typeof(GiveFlowerDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GiveFlowerDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GiveFlowerDto401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(GiveFlowerDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult GiveFlower([FromBody] GiveFlowerDto value)
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

            var record = new FlowerGiver
            {
                FlowerId = value.FlowerId,
                UserId = userId,
                ArticleId = value.ArticleId,
            };
            var Article = _dbContext.Articles.SingleOrDefault(u => u.Id == value.ArticleId);
            var UserFlower = _dbContext.FlowerOwnerships.SingleOrDefault(u => u.UserId == userId && u.Flowerid == value.FlowerId);
            var CreaterFlower = _dbContext.FlowerOwnerships.SingleOrDefault(u => u.UserId == Article.UserId && u.Flowerid == value.FlowerId);


            if (UserFlower.FlowerCount == 0)
            {
                return BadRequest(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "One or more validation errors occurred.",
                    status = 400,
                    errors = "你的花不夠"
                });
            }
            if (userId == Article.UserId)
            {
                return BadRequest(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "One or more validation errors occurred.",
                    status = 400,
                    errors = "很抱歉，不可以送自己花"
                });
            }
            try
            {
                _dbContext.FlowerGivers.Add(record);
                Article.FlowerCount = Article.FlowerCount + 1;
                UserFlower.FlowerCount = UserFlower.FlowerCount - 1;
                CreaterFlower.FlowerCount = CreaterFlower.FlowerCount + 1;
                _dbContext.Entry(Article).State = EntityState.Modified;
                _dbContext.Entry(UserFlower).State = EntityState.Modified;
                _dbContext.Entry(CreaterFlower).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "送花失敗");
            }

            return Ok(new
            {
                StatusCode = 200,
                title = "送花成功"
            });
        }
    }
}