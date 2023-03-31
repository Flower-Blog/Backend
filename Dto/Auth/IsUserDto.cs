using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class IsUserDto
    {
        /// <example>0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089</example>
        [MaxLength(42, ErrorMessage = "{0}不得高於{1}個字元")]
        public string address { get; set; }

    }
    public class IsUserDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public string Nonce { get; set; }

    }
    public class IsUserDto400
    {

        /// <example>https://tools.ietf.org/html/rfc7231#section-6.5.1</example>
        public string type { get; set; }

        /// <example>One or more validation errors occurred.</example>
        public string title { get; set; }

        /// <example>400</example>
        public string status { get; set; }


        /// <example>{"address": ["address必須等於42個字元"]}</example>
        public object errors { get; set; }
    }
    public class IsUserDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }

        /// <example>找不到該使用者</example>
        public string Title { get; set; }

    }
}