using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class GetSingleArticleCommentsDto
    {

        public string Contents { get; set; }

        public int Likes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        /// <example>
        ///     {
        ///         "Name": "Andy",
        ///     },
        /// </example>
        public UserData userdata { get; set; }
    }
    public class GetSingleArticleCommentsDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>
        /// [
        ///     {
        ///         "id": 2,
        ///         "contents": "一些後勁蠻強的短篇冷笑話",
        ///         "likes": 0,
        ///         "createdAt": "2023-04-22T20:49:32.71",
        ///         "updatedAt": "2023-04-22T20:49:32.71",
        ///         "userdata": {
        ///             "name": "Andy1"
        ///         }
        ///     },
        ///     {
        ///         "id": 4,
        ///         "contents": "一些後勁蠻強的短篇冷笑話",
        ///         "likes": 0,
        ///         "createdAt": "2023-04-22T20:49:32.71",
        ///         "updatedAt": "2023-04-22T20:49:32.71",
        ///         "userdata": {
        ///             "name": "Andy1"
        ///         }
        ///     }
        /// ]
        /// </example>
        public object[] comments { get; set; }
    }
    public class GetSingleArticleCommentsDto400
    {
        /// <example>400</example>
        public string StatusCode { get; set; }
        /// <example>文章不存在</example>
        public string title { get; set; }
    }
    public class GetSingleArticleCommentsDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>取得單一文章所有留言失敗</example>
        public string title { get; set; }
    }
}