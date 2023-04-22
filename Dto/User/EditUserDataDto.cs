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
        /// <example>https://png.pngtree.com/thumb_back/fh260/background/20201208/pngtree-beautiful-purple-blooming-christmas-snowflake-image_503982.jpg</example>
        public string BackgroundPhoto { get; set; }
        /// <example>https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Code.org_logo.svg/1200px-Code.org_logo.svg.png</example>
        public string Picture { get; set; }
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
    public class EditUserDataDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>存取發生錯誤</example>
        public string title { get; set; }
    }
}