using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using DotnetWebApi.Dto;
using DotnetWebApi.HandleFunction;
using DotnetWebApi.Helper;
using DotnetWebApi.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IConfiguration _config;
        public UserController(BlogContext dbContext, IMailService mailService, IConfiguration config)
        {
            _dbContext = dbContext;
            _mailService = mailService;
            _config = config;
        }

        /// <summary>
        /// 獲取自身資料(需攜帶Token)
        /// </summary>
        [HttpGet("/user")]
        [Authorize(Roles = "user,admin")]
        [ProducesResponseType(typeof(GetUserDataDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetUserDataDto401), StatusCodes.Status401Unauthorized)]
        public ActionResult GetUserData()
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

            var userdata = from a in _dbContext.Users
                           where a.Address == userAddress
                           select new GetUserDataDto
                           {
                               Id = a.Id,
                               Name = a.Name,
                               Address = a.Address,
                               Email = a.Email,
                               Introduction = a.Introduction,
                               BackgroundPhoto = a.BackgroundPhoto,
                               Picture = a.Picture
                           };
            return Ok(new
            {
                StatusCode = 200,
                userdata
            });
        }

        /// <summary>
        /// 獲取特定使用者資料
        /// </summary>
        [HttpGet("/user/{username}")]
        [ProducesResponseType(typeof(GetCreaterDataDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCreaterDataDto404), StatusCodes.Status404NotFound)]
        public ActionResult GetCreaterDataDto(string username)
        {
            var userdata = (from a in _dbContext.Users
                            where a.Name == username
                            select new GetUserDataDto
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Address = a.Address,
                                Email = a.Email,
                                Introduction = a.Introduction,
                                BackgroundPhoto = a.BackgroundPhoto,
                                Picture = a.Picture
                            }).FirstOrDefault();
            if (userdata == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    title = "找不到該使用者"
                });
            }

            return Ok(new
            {
                StatusCode = 200,
                userdata
            });
        }

        /// <summary>
        /// 編輯自身使用者資料
        /// </summary>
        [HttpPatch("/user")]
        [Authorize(Roles = "user,admin")]
        [ProducesResponseType(typeof(EditUserDataDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EditUserDataDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EditUserDataDto401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(EditUserDataDto404), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EditUserDataDto409), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(EditUserDataDto500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EditUserDataDtoAsync([FromForm] EditUserDataDto value)
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

            var EmailUse = (from x in _dbContext.Users
                            where x.Email == value.Email && x.Address != userAddress
                            select x).FirstOrDefault();
            var NameUse = (from x in _dbContext.Users
                           where x.Name == value.Name && x.Address != userAddress
                           select x).FirstOrDefault();

            if (EmailUse != null || NameUse != null)
            {
                dynamic response = new ExpandoObject();
                response.StatusCode = 409;
                response.Title = "有資料被使用過了 拒絕註冊";
                if (EmailUse != null) response.Email = "信箱被使用過";
                if (NameUse != null) response.Name = "名稱被使用過";
                return Conflict(response);
            }

            var user = _dbContext.Users.SingleOrDefault(u => u.Address == userAddress);

            try
            {
                var _uploadedfiles = Request.Form.Files;
                UserHandle test = new UserHandle();
                bool BackgroundPhoto = true;

                foreach (IFormFile source in _uploadedfiles)
                {
                    string Filepath = test.GetFilePath(user.Address, BackgroundPhoto);

                    if (!System.IO.Directory.Exists(Filepath))
                    {
                        System.IO.Directory.CreateDirectory(Filepath);
                    }
                    string FileType = Path.GetExtension(source.FileName).Substring(1);

                    string imagepath = Filepath + $"//image.{FileType}";

                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                    }
                    BackgroundPhoto = false;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, title = "上傳檔案出錯了", error = ex });
            }

            // 更新使用者資料
            user.Name = value.Name;
            user.Email = value.Email;
            user.Introduction = value.Introduction;
            user.BackgroundPhoto = $"https://{_config["IP"]}:3000/BackgroundPhoto/{userAddress}/image.png";
            user.Picture = $"https://{_config["IP"]}:3000/Picture/{userAddress}/image.png";
            user.UpdatedAt = DateTime.Now;


            try
            {
                // 樂觀併發:
                _dbContext.Entry(user).State = EntityState.Modified;
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
        /// 使用者註冊
        /// </summary>
        [HttpPost("/user/register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegisterDto201), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterDto409), StatusCodes.Status409Conflict)]
        public ActionResult Register(RegisterDto value)
        {
            var userETF = (from x in _dbContext.Users
                           where x.Email == value.Email || x.Name == value.Name || x.Address == value.Address
                           select x).FirstOrDefault();
            var EmailUse = (from x in _dbContext.Users
                            where x.Email == value.Email
                            select x).FirstOrDefault();
            var NameUse = (from x in _dbContext.Users
                           where x.Name == value.Name
                           select x).FirstOrDefault();
            var AddressUse = (from x in _dbContext.Users
                              where x.Address == value.Address
                              select x).FirstOrDefault();

            if (userETF == null)
            {
                var entity = _dbContext.Users.Add(new User
                {
                    Name = value.Name,
                    Address = value.Address,
                    Email = value.Email,
                    BackgroundPhoto = $"https://{_config["IP"]}:3000/BackgroundPhoto/Unknow.png",
                    Picture = $"https://{_config["IP"]}:3000/Picture/Unknow.png",
                    Nonce = Guid.NewGuid().ToString(),
                    Admin = value.admin
                });
                _dbContext.SaveChanges();

                return CreatedAtAction("IsUser", "Auth", new { address = value.Address }, value);

            }
            dynamic response = new ExpandoObject();
            response.StatusCode = 409;
            response.Title = "有資料被使用過了 拒絕註冊";
            if (EmailUse != null) response.Email = "信箱被使用過";
            if (NameUse != null) response.Name = "名稱被使用過";
            if (AddressUse != null) response.Address = "地址被使用過";
            return Conflict(response);
        }

        /// <summary>
        /// 確認電子郵件是否使用過-註冊
        /// </summary>
        /// <param name="email" example="andy910812@gmail.com">會員Email</param>
        /// <returns></returns>
        [HttpGet("/user/register/email/{email}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegisterEmailDto200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterEmailDto400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterEmailDto404), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RegisterEmail(string email)
        {
            if (Regex.IsMatch(email, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$") == false)
            {
                return BadRequest(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "One or more validation errors occurred.",
                    status = 400,
                    errors = new { address = new[] { "email格式不正確" } }
                });
            }
            var userETF = (from x in _dbContext.Users
                           where x.Email == email
                           select x).FirstOrDefault();

            var mailETF = (from x in _dbContext.Mail
                           where x.Email == email
                           select x).FirstOrDefault();
            var test = (from x in _dbContext.Mail
                        select x).Count();

            if (userETF == null)
            {
                Random rd = new Random();
                var VerificationCode = rd.Next(100000, 1000000);

                // 找不到使用者email 就幫他新增進去
                if (mailETF == null)
                {
                    Console.WriteLine("email: " + email);
                    Console.WriteLine("VerificationCode: " + VerificationCode);
                    var entity = _dbContext.Mail.Add(new Mail
                    {
                        Email = email,
                        VerificationCode = VerificationCode,
                    });
                    _dbContext.SaveChanges();
                }
                // 找到使用者email 就幫他更新一組亂數
                else
                {
                    mailETF.VerificationCode = VerificationCode;
                    mailETF.UpdatedAt = DateTime.Now;
                    _dbContext.Entry(mailETF).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }


                try
                {
                    MailRequest request = new MailRequest();
                    request.ToEmail = email;
                    request.Subject = "Flower Blog 電子郵件驗證";
                    request.Body = _mailService.MailBody(VerificationCode.ToString());
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
        /// 確認使用者名稱是否使用過-註冊
        /// </summary>
        /// <param name="name" example="Andy">會員名稱</param>
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