using AutoMapper.Configuration.Annotations;
using DotnetWebApi.Models;

namespace DotnetWebApi.Dto
{
    public class ArtilceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
    public class GiveFlowerRecordDto
    {
        /// <example>1</example>
        public int FlowerId { get; set; }
        /// <example>
        ///     {
        ///         "Name": "Andy",
        ///         "Picture": "https://localhost:3000/Picture/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png"
        ///     },
        /// </example>
        public ArtilceDto article { get; set; }
        /// <example>2023/05/19 17:00</example>
        public DateTime CreatedAt { get; set; }
        /// <example>
        ///     {
        ///         "Name": "Andy",
        ///         "Picture": "https://localhost:3000/Picture/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png"
        ///     },
        /// </example>
        public UserDataDto userdata { get; set; }
    }
    public class GiveFlowerRecordDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>
        /// [
        ///     {
        ///         "flowerId": 1,
        ///         "article":{
        ///             "id": 2,
        ///             "name": "Andy",
        ///             "title": "Test"     
        ///         },
        ///         "createdAt": "2023-05-27T06:49:53.48",
        ///         "userdata":{
        ///             "name": "Yaa",
        ///             "picture": "https://jone.ddns.net:9530/Picture/0x4202043d9ff98a4e8c64b075dbf4cb3ee5eff528/image.gif"     
        ///         }
        ///     },
        ///     {
        ///         "flowerId": 1,
        ///         "article":{
        ///             "id": 2,
        ///             "name": "Andy",
        ///             "title": "Test"     
        ///         },
        ///         "createdAt": "2023-05-27T06:49:53.48",
        ///         "userdata":{
        ///             "name": "Yaa",
        ///             "picture": "https://jone.ddns.net:9530/Picture/0x4202043d9ff98a4e8c64b075dbf4cb3ee5eff528/image.gif"     
        ///         }
        ///     }
        /// ]
        /// </example>
        public object[] flowerRecords { get; set; }
    }
    public class GiveFlowerRecordDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>管理者獲取送花紀錄失敗</example>
        public string title { get; set; }
    }
}