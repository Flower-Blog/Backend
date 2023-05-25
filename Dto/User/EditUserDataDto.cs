using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class EditUserDataDto
    {
        /// <example>Andy</example>
        public string Name { get; set; }
        /// <example>andy910812@gmail.com</example>
        public string Email { get; set; }
        /// <example>自我介紹</example>
        public string Introduction { get; set; }

        public IFormFile BackgroundPhoto { get; set; }

        public IFormFile Picture { get; set; }
    }
    public class EditUserDataDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>修改成功</example>
        public string title { get; set; }
    }
    public class EditUserDataDto400
    {
        /// <example>400</example>
        public string StatusCode { get; set; }
        /// <example>這不是你的帳號歐</example>
        public string title { get; set; }
    }
    public class EditUserDataDto401
    {

    }
    public class EditUserDataDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }
        /// <example>同步修改失敗</example>
        public string title { get; set; }
    }
    public class EditUserDataDto409
    {
        /// <example>409</example>
        public string StatusCode { get; set; }

        /// <example>有資料被使用過了 拒絕註冊</example>
        public string Title { get; set; }
        /// <example>信箱被使用過</example>
        public string Email { get; set; }
        /// <example>名稱被使用過</example>
        public string Name { get; set; }
    }
    public class EditUserDataDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>存取發生錯誤</example>
        public string title { get; set; }
    }
}