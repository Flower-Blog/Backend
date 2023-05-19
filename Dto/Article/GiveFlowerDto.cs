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
    public class GiveFlowerDto400
    {

        /// <example>https://tools.ietf.org/html/rfc7231#section-6.5.1</example>
        public string type { get; set; }

        /// <example>One or more validation errors occurred.</example>
        public string title { get; set; }

        /// <example>400</example>
        public string status { get; set; }


        /// <example>"你的花不夠"</example>
        public object errors { get; set; }
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