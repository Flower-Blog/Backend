using AutoMapper.Configuration.Annotations;
using DotnetWebApi.Models;

namespace DotnetWebApi.Dto
{
    public class UserDataDto
    {
        public string Name { get; set; }
        public string Picture { get; set; }
    }
    public class GetAllArticlesDto
    {
        /// <example>1</example>
        public int Id { get; set; }
        /// <example>一些後勁蠻強的短篇冷笑話</example>
        public string Title { get; set; }
        /// <example>世界不需要中國民主化，世界更需要一群愚昧的中國人</example>
        public string SubStandard { get; set; }
        /// <exaple>簡潔的內容</exaple>
        public string Contents { get; set; }
        /// <exaple>27</exaple>
        public int FlowerCount { get; set; }
        /// <exaple>2023-04-22T20:49:32.71</exaple>
        public DateTime CreatedAt { get; set; }
        /// <exaple>2023-04-22T20:49:32.71</exaple>
        public DateTime UpdatedAt { get; set; }
        /// <example>
        ///     {
        ///         "Name": "Andy",
        ///         "Picture": "https://localhost:3000/Picture/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png"
        ///     },
        /// </example>
        public UserDataDto userdata { get; set; }
    }
    public class GetAllArticlesDto200
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
        ///         "updatedAt": "2023-04-22T20:49:32.71",
        ///         "userdata": {
        ///             "name": "Andy1"
        ///         }
        ///     },
        ///     {
        ///         "id": 4,
        ///         "title": "一些後勁蠻強的短篇冷笑話",
        ///         "subStandard": "哈哈",
        ///         "contents": "string",
        ///         "createdAt": "2023-04-22T20:49:32.71",
        ///         "updatedAt": "2023-04-22T20:49:32.71",
        ///         "userdata": {
        ///             "name": "Andy1"
        ///         }
        ///     }
        /// ]
        /// </example>
        public object[] articles { get; set; }
    }
    public class GetAllArticlesDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>取得所有文章失敗</example>
        public string title { get; set; }
    }
}