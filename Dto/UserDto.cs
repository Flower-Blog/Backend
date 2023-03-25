using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class UserDto
    {
        /// <example>Andy</example>
        public string Name { get; set; }

        /// <example>0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089</example>
        public string Address { get; set; }

        /// <example>andy@gmail.com</example>
        public string Email { get; set; }

        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public string Nonce { get; set; }
    }
}