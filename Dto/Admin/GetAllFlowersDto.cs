using AutoMapper.Configuration.Annotations;
using DotnetWebApi.Models;

namespace DotnetWebApi.Dto
{
    public class GetAllFlowersDto
    {
        /// <example>1</example>
        public int Id { get; set; }
        /// <example>玫瑰花</example>
        public string Name { get; set; }
        /// <example>向日葵</example>
        public string Language { get; set; }
    }
    public class GetAllFlowersDto200
    {
        /// <example>200</example>
        public string StatusCode { get; set; }
        /// <example>
        /// [
        ///     {
        ///       "id": 1,
        ///       "name": "玫瑰花",
        ///       "language": "愛情、浪漫、美麗、熱情、溫馨、關懷、感激、勇敢、誠實、溫柔"
        ///     },
        ///     {
        ///       "id": 2,
        ///       "name": "向日葵",
        ///       "language": "忠誠、陽光、希望、堅強、自信、友善、積極、夢想、幸福、寬容。"
        ///     },
        ///     {
        ///       "id": 3,
        ///       "name": "康乃馨",
        ///       "language": "真愛、友情、母愛、幸福、忍耐、感激、祝福、美麗、喜悅、寬容"
        ///     },
        ///     {
        ///       "id": 4,
        ///       "name": "百合花",
        ///       "language": "純潔、寧靜、高雅、祝福、無暇、優雅、美好、真誠、祥和、善良"
        ///     },
        ///     {
        ///       "id": 5,
        ///       "name": "桃花",
        ///       "language": "愛情、美麗、純真、浪漫、喜悅、甜蜜、吉祥、和諧、幸福、恩愛"
        ///     }
        /// ]
        /// </example>
        public object[] flowers { get; set; }
    }
    public class GetAllFlowersDto500
    {
        /// <example>500</example>
        public string StatusCode { get; set; }
        /// <example>取得所有文章失敗</example>
        public string title { get; set; }
    }
}