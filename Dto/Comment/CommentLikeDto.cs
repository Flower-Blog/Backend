using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class CommentLikeDto
    {
        /// <example>1</example>
        public int ArticleId { get; set; }
        /// <example>哈哈真好笑！！！</example>
        public string Contents { get; set; }
    }
    public class CommentLikeDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>按攢成功</example>
        public string title { get; set; }
    }
    public class CommentLikeDto400
    {
        /// <example>400</example>
        public string StatusCode { get; set; }
        /// <example>你已經按過贊了</example>
        public string title { get; set; }
    }
    public class CommentLikeDto401
    {

    }
    public class CommentLikeDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>按讚失敗</example>
        public string title { get; set; }
    }
}