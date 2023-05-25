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
    public class CommentController : ControllerBase
    {
        private readonly BlogContext _dbContext;
        private readonly IMailService _mailService;
        public CommentController(BlogContext dbContext, IMailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
        }

        /// <summary>
        /// 新增留言(需攜帶Token)
        /// </summary>
        [HttpPost("/comment")]
        [ProducesResponseType(typeof(CreateCommentDto201), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateCommentDto401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CreateCommentDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateCommentDto500), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "user,admin")]
        public ActionResult CreateComment(CreateCommentDtoDto value)
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

            var Article = (from a in _dbContext.Articles
                           where a.Id == value.ArticleId
                           select a).FirstOrDefault();
            if (Article == null)
            {
                return StatusCode(400, "文章不存在");
            }
            try
            {
                var entity = _dbContext.Comments.Add(new Comment
                {
                    UserId = userId,
                    ArticleId = value.ArticleId,
                    Contents = value.Contents,
                    Likes = 0,
                });

                _dbContext.SaveChanges();
                return StatusCode(201, "新增留言成功");
            }
            catch
            {
                return StatusCode(500, "新增留言失敗");
            }
        }
        /// <summary>
        /// 編輯留言
        /// </summary>
        /// <param name="id" example="2">留言編號</param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPatch("/comment/{id}")]
        [Authorize(Roles = "user,admin")]
        [ProducesResponseType(typeof(EditCommentDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EditCommentDto401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(EditCommentDto403), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(EditCommentDto404), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EditCommentDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult EditComment(int id, [FromBody] EditCommentDto value)
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

            var comment = _dbContext.Comments.SingleOrDefault(u => u.Id == id);

            if (comment == null)
                return NotFound(new { StatusCode = 404, title = "找不到留言" });

            var user = _dbContext.Users.SingleOrDefault(u => u.Address == userAddress);

            if (comment.UserId != user.Id)
                return new ObjectResult(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "你不是這邊留言的作者",
                    status = 403,
                })
                {
                    StatusCode = 403
                };

            comment.Contents = value.Contents;
            comment.UpdatedAt = DateTime.Now;

            try
            {
                // 樂觀併發:
                _dbContext.Entry(comment).State = EntityState.Modified;
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
        /// 刪除個人留言
        /// </summary>
        /// <param name="id" example="2">留言編號</param>
        /// <returns></returns>
        [HttpDelete("/comment/{id}")]
        [Authorize(Roles = "user,admin")]
        [ProducesResponseType(typeof(DeleteCommentDto204), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(DeleteCommentDto401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(DeleteCommentDto403), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(DeleteCommentDto404), StatusCodes.Status404NotFound)]
        public IActionResult DeleteComment(int id)
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

            var comment = _dbContext.Comments.SingleOrDefault(u => u.Id == id);

            if (comment == null)
                return NotFound(new { StatusCode = 404, title = "找不到留言" });

            if (comment.UserId != user.Id)
                return new ObjectResult(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "你不是這邊留言的作者",
                    status = 403,
                })
                {
                    StatusCode = 403
                };

            // 按讚
            var commentLikesToRemove = _dbContext.CommentLikes.Where(a => a.CommentId == comment.Id);
            _dbContext.CommentLikes.RemoveRange(commentLikesToRemove);
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();

            return StatusCode(200, "刪除成功");
        }

        /// <summary>
        /// 留言按讚
        /// </summary>
        /// <param name="id" example="2">留言編號</param>
        [HttpGet("/comment/{id}/like")]
        [Authorize(Roles = "user,admin")]
        [ProducesResponseType(typeof(CommentLikeDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommentLikeDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommentLikeDto500), StatusCodes.Status500InternalServerError)]
        public ActionResult CommentLike(int id)
        {
            try
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



                var comment = (from a in _dbContext.Comments
                               where a.Id == id
                               select a).FirstOrDefault();

                if (comment == null)
                {
                    return StatusCode(400, "留言不存在");
                }

                // 查看是否按過讚
                var Check = (from a in _dbContext.CommentLikes
                             where a.UserId == user.Id && a.CommentId == comment.Id
                             select a).FirstOrDefault();

                if (Check != null)
                {
                    return StatusCode(400, "你已經按過贊了");
                }

                comment.Likes = comment.Likes + 1;
                _dbContext.Entry(comment).State = EntityState.Modified;


                _dbContext.CommentLikes.Add(new CommentLike
                {
                    UserId = user.Id,
                    CommentId = comment.Id,
                });


                _dbContext.SaveChanges();

                return Ok(new
                {
                    StatusCode = 200,
                    title = "按攢成功"
                });

            }
            catch
            {
                return StatusCode(500, "按讚失敗");
            }
        }
    }
}