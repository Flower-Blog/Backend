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
    public class RegisterEmailDto404
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>信箱已被使用過</example>
        public string Title { get; set; }
    }
}