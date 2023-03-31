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
    public class ConfirmationEMailDto400
    {

        /// <example>https://tools.ietf.org/html/rfc7231#section-6.5.1</example>
        public string type { get; set; }

        /// <example>One or more validation errors occurred.</example>
        public string title { get; set; }

        /// <example>400</example>
        public string status { get; set; }

        /// <example>{"email": ["email格式不正確"],"verificationCode": ["驗證碼格式不正確 提示只有6碼"]}</example>
        public object errors { get; set; }

    }
    public class ConfirmationEMailDto401
    {
        /// <example>401</example>
        public string StatusCode { get; set; }

        /// <example>超過10分鐘的驗證時間</example>
        public string Title { get; set; }

    }
    public class ConfirmationEMailDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }

        /// <example>信箱驗證錯誤 或是 此信箱根本沒有發送過驗證碼歐!</example>
        public string Title { get; set; }

    }
}