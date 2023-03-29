using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class RegisterDto
    {
        /// <example>Andy</example>
        public string Name { get; set; }
        /// <example>0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089</example>
        [MaxLength(42, ErrorMessage = "{0}不得高於{1}個字元")]
        public string Address { get; set; }
        /// <example>andy910812@gmail.com</example>
        [EmailAddress(ErrorMessage = "email格式不正確")]
        public string Email { get; set; }
    }
    public class RegisterDto201
    {
        /// <example>201</example>
        public string StatusCode { get; set; }

        /// <example>註冊成功</example>
        public string Title { get; set; }
        /// <example>Andy</example>
        public string Name { get; set; }
        /// <example>0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089</example>
        public string Address { get; set; }
        /// <example>andy910812@gmail.com</example>
        public string Email { get; set; }
    }
    public class RegisterDto400
    {
        /// <example>https://tools.ietf.org/html/rfc7231#section-6.5.1</example>
        public string type { get; set; }

        /// <example>One or more validation errors occurred.</example>
        public string title { get; set; }

        /// <example>400</example>
        public string status { get; set; }

        /// <example>{"email": ["email格式不正確"],"Address": ["Address不得高於42個字元"]}</example>
        public object errors { get; set; }
    }
    public class RegisterDto409
    {
        /// <example>409</example>
        public string StatusCode { get; set; }

        /// <example>有資料被使用過了 拒絕註冊</example>
        public string Title { get; set; }
        /// <example>信箱被使用過</example>
        public string Email { get; set; }
        /// <example>名稱被使用過</example>
        public string Name { get; set; }
        /// <example>地址被使用過</example>
        public string Address { get; set; }
    }
}