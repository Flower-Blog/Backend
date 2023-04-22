using AutoMapper.Configuration.Annotations;
using DotnetWebApi.Models;

namespace DotnetWebApi.Dto
{
    public class GetAllArticlesDto
    {
        /// <example>1</example>
        public int Id { get; set; }
        /// <example>0x80220D006B75fC516ac1662a189c585Ae9f10fE0</example>
        public string address { get; set; }
        /// <example>一些後勁蠻強的短篇冷笑話</example>
        public string Title { get; set; }
        /// <example>世界不需要中國民主化，世界更需要一群愚昧的中國人</example>
        public string SubStandard { get; set; }
        /// <exaple>簡潔的內容</exaple>
        public string Contents { get; set; }
    }
    public class GetAllArticlesDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>
        /// [
        ///     {
        ///         "id": 2,
        ///         "address": "0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089",
        ///         "title": "一些後勁蠻強的短篇冷笑話",
        ///         "subStandard": "test",
        ///         "contents": "string"
        ///     },
        ///     {
        ///         "id": 4,
        ///         "address": "0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089",
        ///         "title": "一些後勁蠻強的短篇冷笑話",
        ///         "subStandard": "哈哈",
        ///         "contents": "string"
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