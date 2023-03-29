using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class ConfirmationEMailDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public string Name { get; set; }

    }
    public class ConfirmationEMailDto401
    {
        /// <example>401</example>
        public string StatusCode { get; set; }

        /// <example>超過10分鐘的驗證時間</example>
        public string Title { get; set; }

    }
    public class ConfirmationEMailDto400
    {
        /// <example>400</example>
        public string StatusCode { get; set; }

        /// <example>信箱驗證錯誤</example>
        public string Title { get; set; }

    }
}