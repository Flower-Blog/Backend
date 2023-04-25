using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotnetWebApi.Helper
{
    public class JwtHelper
    {
        private readonly IConfiguration _config;
        public JwtHelper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string address, int expireMinutes = 120,bool admin = false)
        {
            var issuer = _config["JwtSettings_Issuer"];
            // 加密的key，拿來比對jwt-token沒有
            var signKey = _config["JwtSettings_SignKey"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));

            var roles = admin ? "admin" : "user";
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(JwtRegisteredClaimNames.Aud, "audience"),
                new Claim("address", address),
                new Claim("roles", roles)
            };


            var jwt = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}