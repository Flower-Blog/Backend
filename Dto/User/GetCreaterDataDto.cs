using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class GetCreaterDataDto
    {
        /// <example>1</example>
        public int Id { get; set; }
        /// <example>Andy</example>
        public string Name { get; set; }
        /// <example>0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089</example>
        public string Address { get; set; }
        /// <example>andy910812@gmail.com</example>
        public string Email { get; set; }
        /// <example>自我介紹</example>
        public string Introduction { get; set; }
        /// <example>https://png.pngtree.com/thumb_back/fh260/background/20201208/pngtree-beautiful-purple-blooming-christmas-snowflake-image_503982.jpg</example>
        public string BackgroundPhoto { get; set; }
        /// <example>https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Code.org_logo.svg/1200px-Code.org_logo.svg.png</example>
        public string Picture { get; set; }
    }
    public class GetCreaterDataDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>1</example>
        public int Id { get; set; }
        /// <example>Andy</example>
        public string Name { get; set; }
        /// <example>0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089</example>
        public string Address { get; set; }
        /// <example>andy910812@gmail.com</example>
        public string Email { get; set; }
        /// <example>自我介紹</example>
        public string Introduction { get; set; }
        /// <example>https://png.pngtree.com/thumb_back/fh260/background/20201208/pngtree-beautiful-purple-blooming-christmas-snowflake-image_503982.jpg</example>
        public string BackgroundPhoto { get; set; }
        /// <example>https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Code.org_logo.svg/1200px-Code.org_logo.svg.png</example>
        public string Picture { get; set; }
    }
    public class GetCreaterDataDto404
    {
        /// <example>404</example>
        public string StatusCode { get; set; }
        /// <example>找不到使用者</example>
        public string title { get; set; }
    }
}