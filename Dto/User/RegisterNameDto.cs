using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class RegisterNameDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>名稱未使用過</example>
        public string Title { get; set; }

    }
    public class RegisterNameDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }

        /// <example>名稱未已被使用過</example>
        public string Title { get; set; }
    }
}