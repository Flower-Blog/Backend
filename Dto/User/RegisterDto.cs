using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class RegisterDto
    {
        /// <example>Andy</example>
        public string Name { get; set; }
        /// <example>0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089</example>
        public string Address { get; set; }
        /// <example>andy@gmail.com</example>
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
        /// <example>andy@gmail.com</example>
        public string Email { get; set; }
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