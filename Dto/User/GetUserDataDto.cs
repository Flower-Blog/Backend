using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class Article_FlowerDto
    {
        public int id { get; set; }
        public string Title { get; set; }
    }
    public class FlowerDto
    {
        public int Id { get; set; }
        public int FlowerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Article_FlowerDto Article { get; set; }
    }
    public class ReceiveFlowersRecordsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FlowerId { get; set; }
        public Article_FlowerDto Article { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class GetUserDataDto
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
        /// <example>true</example>
        public bool Admin { get; set; }
    }
    public class GetUserDataDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>
        /// [
        ///     {
        ///     "id": 12,
        ///     "name": "Andy1",
        ///     "address": "0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089",
        ///     "email": "andy910812@gmail.com",
        ///     "introduction": "自我介紹",
        ///     "backgroundPhoto": "https://localhost:3000/BackgroundPhoto/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png",
        ///     "picture": "https://localhost:3000/Picture/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png",
        ///     "admin": false
        ///     }
        /// ]
        /// </example>
        public object userdata { get; set; }
        /// <example>
        /// [
        ///     {
        ///         "id": 6,
        ///         "contents": "哈哈真好笑！！！",
        ///         "likes": 1,
        ///         "createdAt": "2023-05-19T19:50:37.35",
        ///         "userdata": {
        ///             "name": "Andy1",
        ///             "picture": "https://localhost:3000/Picture/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png"
        ///         }
        ///     }
        /// ]
        /// </example>
        public object commentRecords { get; set; }
        /// <example>
        /// [
        ///     {
        ///         "id": 15,
        ///         "flowerId": 1,
        ///         "createdAt": "2023-05-19T19:20:15.22",
        ///         "article": {
        ///             "id": 15,
        ///             "title": "一些後勁蠻強的短篇冷笑話"
        ///         }
        ///     }
        /// ]
        /// </example>
        public object flowerRecords { get; set; }
    }
    public class GetUserDataDto401
    {

    }
}