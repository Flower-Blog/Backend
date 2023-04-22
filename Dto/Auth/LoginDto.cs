using System.ComponentModel.DataAnnotations;


namespace DotnetWebApi.Dto
{
    public class LoginDto
    {
        /// <example>0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089</example>
        [MaxLength(42, ErrorMessage = "{0}必須等於{1}個字元")]
        [MinLength(42, ErrorMessage = "{0}必須等於{1}個字元")]
        public string address { get; set; }
        /// <example>0xee285f7ee0d69071cb1ca4a8f92013d47164393ee8c2337bd1e5b5cf5e476232709e5d32932fe5f5749a5d41f518cd50bd1dd7dd967c8355c8666b39b93033fb1c</example>
        public string signature { get; set; }
        
    }
    public class LoginDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }

        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQW5keSIsImVtYWlsIjoiYW5keUBnbWFpbC5jb20iLCJhZGRyZXNzIjoiMHhFRmE0QWJhYzdGZWRCOEYwNTE0YmVFNzIxMmRjMTlENTIzREQzMDg5IiwiZXhwIjoxNjc5OTUzNDk5LCJpc3MiOiJGbG93ZXIgQmxvZyIsImF1ZCI6IlVzZXIifQ.8aJTuL0MlxzuKxys9HOi9N--RVBXGZP1JM9BTatXamw</example>
        public string Token { get; set; }

    }
    public class LoginDto400
    {

        /// <example>https://tools.ietf.org/html/rfc7231#section-6.5.1</example>
        public string type { get; set; }

        /// <example>One or more validation errors occurred.</example>
        public string title { get; set; }

        /// <example>400</example>
        public string status { get; set; }


        /// <example>{"address": ["address必須等於42個字元"]}</example>
        public object errors { get; set; }
    }

    public class LoginDto401
    {
        /// <example>401</example>
        public string StatusCode { get; set; }

        /// <example>登入驗證失敗</example>
        public string Title { get; set; }

    }
    public class LoginDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }

        /// <example>找不到該使用者</example>
        public string Title { get; set; }

    }
}