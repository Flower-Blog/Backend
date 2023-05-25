using AutoMapper.Configuration.Annotations;
using DotnetWebApi.Models;

namespace DotnetWebApi.Dto
{
    public class CommentsDto
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedAt { get; set; }
        /// <example>
        ///     {
        ///         "Name": "Andy",
        ///         "Picture": "https://localhost:3000/Picture/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png"
        ///     },
        /// </example>
        public UserDataDto userdata { get; set; }
    }
    public class GetArticleDto
    {
        /// <example>1</example>
        public int Id { get; set; }
        /// <example>一些後勁蠻強的短篇冷笑話</example>
        public string Title { get; set; }
        /// <example>世界不需要中國民主化，世界更需要一群愚昧的中國人</example>
        public string SubStandard { get; set; }
        /// <exaple>簡潔的內容</exaple>
        public string Contents { get; set; }
        /// <exaple>23</exaple>
        public int FlowerCount { get; set; }
        /// <exaple>2023-04-22T20:49:32.71</exaple>
        public DateTime CreatedAt { get; set; }
        /// <exaple>2023-04-22T20:49:32.71</exaple>
        public DateTime UpdatedAt { get; set; }
        public CommentsDto[] Comments { get; set; }

    }
    public class GetCommentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class GetArticleDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>
        /// {
        ///     "id": 2,
        ///     "title": "一些後勁蠻強的短篇冷笑話",
        ///     "subStandard": "test",
        ///     "contents": "string",
        ///     "FlowerCount" : "23",
        ///     "createdAt": "2023-04-22T20:49:32.71",
        ///     "updatedAt": "2023-04-22T20:49:32.71",
        ///     "comments": [
        ///         {
        ///             "id": 8,
        ///             "contents": "哈哈！！",
        ///             "likes": 0,
        ///             "createdAt": "2023-05-19T20:22:30.803",
        ///             "userdata": {
        ///                 "name": "Andy1",
        ///                 "picture": "https://localhost:3000/Picture/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png"
        ///             }
        ///         },
        ///         {
        ///             "id": 6,
        ///             "contents": "哈哈真好笑！！！",
        ///             "likes": 1,
        ///             "createdAt": "2023-05-19T19:50:37.35",
        ///             "userdata": {
        ///                 "name": "Andy1",
        ///                 "picture": "https://localhost:3000/Picture/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png"
        ///             }
        ///         }
        ///     ]
        /// }
        /// </example>
        public object articles { get; set; }
    }
    public class GetArticleDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }
        /// <example>找不到文章</example>
        public string title { get; set; }
    }
    public class GetArticleDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>取得單一文章</example>
        public string title { get; set; }
    }
}