using System.ComponentModel.DataAnnotations;

namespace DotnetWebApi.Dto
{
    public class GetUserFlowerDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>
        ///[
        ///    {
        ///      "flowerid": 1,
        ///      "flowerCount": 5
        ///    },
        ///    {
        ///      "flowerid": 2,
        ///      "flowerCount": 3
        ///    },
        ///    {
        ///      "flowerid": 3,
        ///      "flowerCount": 0
        ///    },
        ///    {
        ///      "flowerid": 4,
        ///      "flowerCount": 1
        ///    },
        ///    {
        ///      "flowerid": 5,
        ///      "flowerCount": 0
        ///    }
        ///]
        ///</example>
        public object flowerRecords { get; set; }
    }
    public class GetUserFlowerDto401
    {

    }
}