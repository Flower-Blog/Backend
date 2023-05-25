using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class EditCommentDto
    {
        /// <example>哈哈很好笑!!!</example>
        public string Contents { get; set; }
    }
    public class EditCommentDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>修改成功</example>
        public string title { get; set; }
    }
    public class EditCommentDto401
    {

    }
    public class EditCommentDto403
    {
        /// <example>400</example>
        public string StatusCode { get; set; }
        /// <example>你不是這邊留言的作者</example>
        public string title { get; set; }
        /// <example>"https://tools.ietf.org/html/rfc7231#section-6.5.1"</example>
        public string type { get; set; }
    }
    public class EditCommentDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }
        /// <example>找不到留言</example>
        public string title { get; set; }
    }
    public class EditCommentDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>存取發生錯誤</example>
        public string title { get; set; }
    }
}