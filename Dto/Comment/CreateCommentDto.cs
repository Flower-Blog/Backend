using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class CreateCommentDtoDto
    {
        /// <example>1</example>
        public int ArticleId { get; set; }
        /// <example>哈哈真好笑！！！</example>
        public string Contents { get; set; }
    }
    public class CreateCommentDto201
    {
        /// <example>201</example>
        public string StatusCode { get; set; }

        /// <example>新增留言成功</example>
        public string title { get; set; }
    }
    public class CreateCommentDto400
    {
        /// <example>400</example>
        public string StatusCode { get; set; }
        /// <example>文章不存在</example>
        public string title { get; set; }
    }
    public class CreateCommentDto401
    {

    }
    public class CreateCommentDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>新增留言失敗</example>
        public string title { get; set; }
    }
}