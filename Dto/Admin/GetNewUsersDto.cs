using AutoMapper.Configuration.Annotations;
using DotnetWebApi.Models;

namespace DotnetWebApi.Dto
{
    public class GetNewUsersDto
    {
        /// <example>小名</example>
        public string Name { get; set; }
        /// <example>0x80220D006B75fC516ac1662a189c585Ae9f10fE0</example>
        public string Address { get; set; }
        /// <example>Test@gmail.com</example>
        public string Email { get; set; }
        /// <example>大家好我是小明</example>
        public string Introduction { get; set; }
        /// <example>https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Code.org_logo.svg/1200px-Code.org_logo.svg.png</example>
        public string Picture { get; set; }
        /// <example>2023/05/19 17:00</example>
        public DateTime CreatedAt { get; set; }
    }
    public class GetNewUsersDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>
        /// [
        ///     {
        ///         "name": "Andy",
        ///         "address": "0x34B605B3d13923a60a629794C15B103C44beaE1c",
        ///         "email": "Test@gmail.com",
        ///         "introduction": null,
        ///         "picture": "https://localhost:3000/Picture/Unknow.png",
        ///         "createdAt": "2023-05-10T18:29:21.66"
        ///     },
        ///     {
        ///         "name": "AndyA",
        ///         "address": "0xABa4Abac7FedB8F0514beE7212dc19D523DD3089",
        ///         "email": "andyA@gmail.com",
        ///         "introduction": null,
        ///         "picture": "https://localhost:3000/Picture/Unknow.png",
        ///         "createdAt": "2023-04-25T23:48:42.577"
        ///     },
        ///     {
        ///         "name": "Andy1",
        ///         "address": "0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089",
        ///         "email": "andy910812@gmail.com",
        ///         "introduction": "自我介紹",
        ///         "picture": "https://localhost:3000/Picture/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png",
        ///         "createdAt": "2023-04-25T23:41:35.347"
        ///     }
        /// ]
        /// </example>
        public object[] users { get; set; }
    }
    public class GetNewUsersDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>取得使用者紀錄失敗</example>
        public string title { get; set; }
    }
}