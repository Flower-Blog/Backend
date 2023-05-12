using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class EditArticleDto
    {
        /// <example>一些後勁蠻強的短篇冷笑話</example>
        public string Title { get; set; }
        /// <example>世界不需要中國民主化，世界更需要一群愚昧的中國人</example>
        public string SubStandard { get; set; }
        /// <example>內容</example>
        public string Contents { get; set; }
    }
    public class EditArticleDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>修改成功</example>
        public string title { get; set; }
    }
    public class EditArticleDto401
    {

    }
    public class EditArticleDto403
    {
        /// <example>400</example>
        public string StatusCode { get; set; }
        /// <example>你不是這邊文章的作者</example>
        public string title { get; set; }
        /// <example>"https://tools.ietf.org/html/rfc7231#section-6.5.1"</example>
        public string type { get; set; }
    }
    public class EditArticleDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }
        /// <example>找不到文章</example>
        public string title { get; set; }
    }
    public class EditArticleDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>存取發生錯誤</example>
        public string title { get; set; }
    }
}