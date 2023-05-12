using AutoMapper.Configuration.Annotations;
using DotnetWebApi.Models;

namespace DotnetWebApi.Dto
{
    public class GetUserArticlesDto
    {
        /// <example>1</example>
        public int Id { get; set; }
        /// <example>一些後勁蠻強的短篇冷笑話</example>
        public string Title { get; set; }
        /// <example>世界不需要中國民主化，世界更需要一群愚昧的中國人</example>
        public string SubStandard { get; set; }
        /// <exaple>簡潔的內容</exaple>
        public string Contents { get; set; }
        /// <exaple>2023-04-22T20:49:32.71</exaple>
        public DateTime CreatedAt { get; set; }
        /// <exaple>2023-04-22T20:49:32.71</exaple>
        public DateTime UpdatedAt { get; set; }
    }
    public class GetUserArticlesDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>
        /// [
        ///     {
        ///         "id": 2,
        ///         "title": "一些後勁蠻強的短篇冷笑話",
        ///         "subStandard": "test",
        ///         "contents": "string",
        ///         "createdAt": "2023-04-22T20:49:32.71",
        ///         "updatedAt": "2023-04-22T20:49:32.71"
        ///     },
        ///     {
        ///         "id": 4,
        ///         "title": "一些後勁蠻強的短篇冷笑話",
        ///         "subStandard": "哈哈",
        ///         "contents": "string",
        ///         "createdAt": "2023-04-22T20:49:32.71",
        ///         "updatedAt": "2023-04-22T20:49:32.71"
        ///     }
        /// ]
        /// </example>
        public object[] articles { get; set; }
    }
    public class GetUserArticlesDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }
        /// <example>使用者沒有文章</example>
        public string title { get; set; }
    }
    public class GetUserArticlesDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>取得個人所有文章</example>
        public string title { get; set; }
    }
}