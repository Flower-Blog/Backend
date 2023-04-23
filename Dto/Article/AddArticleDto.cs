using AutoMapper.Configuration.Annotations;
using DotnetWebApi.Models;

namespace DotnetWebApi.Dto
{
    public class AddArticleDto
    {
        /// <example>一些後勁蠻強的短篇冷笑話</example>
        public string Title { get; set; }
        /// <example>世界不需要中國民主化，世界更需要一群愚昧的中國人</example>
        public string SubStandard { get; set; }
        /// <example>內容</example>
        public string Contents { get; set; }
    }
    public class AddArticleDto201
    {
        /// <example>一些後勁蠻強的短篇冷笑話</example>
        public string Title { get; set; }
        /// <example>世界不需要中國民主化，世界更需要一群愚昧的中國人</example>
        public string SubStandard { get; set; }
        /// <exaple>簡潔的內容</exaple>
        public string Contents { get; set; }
    }
    public class AddArticleDto400
    {
        /// <example>400</example>
        public string StatusCode { get; set; }
        /// <example>這不是你的帳號歐</example>
        public string title { get; set; }
    }
    public class AddArticleDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>新增文章失敗</example>
        public string title { get; set; }
    }
}