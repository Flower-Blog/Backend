using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class IsUserDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public string Nonce { get; set; }

    }
    public class IsUserDto401
    {
        /// <example>401</example>
        public string StatusCode { get; set; }

        /// <example>已經註冊過</example>
        public string Title { get; set; }

    }
}