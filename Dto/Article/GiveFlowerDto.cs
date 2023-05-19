using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class GiveFlowerDto
    {
        /// <example>1</example>
        public int FlowerId { get; set; }
        /// <example>哈哈真好笑！！！</example>
        public int ArticleId { get; set; }
    }
    public class GiveFlowerDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>送花成功</example>
        public string title { get; set; }
    }
    public class GiveFlowerDto401
    {

    }
    public class GiveFlowerDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>送花失敗</example>
        public string title { get; set; }
    }
}