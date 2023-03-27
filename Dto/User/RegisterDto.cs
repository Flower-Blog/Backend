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
    public class RegisterDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>註冊成功</example>
        public string Title { get; set; }

    }
    public class RegisterDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }

        /// <example>拒絕註冊</example>
        public string Title { get; set; }
    }
}