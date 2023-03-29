using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class RegisterEmailDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>已發送驗證碼到郵件</example>
        public string Title { get; set; }

    }
    public class RegisterEmailDto400
    {
        /// <example>https://tools.ietf.org/html/rfc7231#section-6.5.1</example>
        public string type { get; set; }

        /// <example>One or more validation errors occurred.</example>
        public string title { get; set; }

        /// <example>400</example>
        public string status { get; set; }

        /// <example>{"email": ["email格式不正確"]}</example>
        public object errors { get; set; }

    }
    public class RegisterEmailDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }

        /// <example>信箱已被使用過</example>
        public string Title { get; set; }
    }
}