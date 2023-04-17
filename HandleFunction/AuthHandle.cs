using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Nethereum.Signer;

namespace DotnetWebApi.HandleFunction
{
    class AuthHandle
    {
        // TODO: 驗證簽證
        public static bool VerifySignature(string nonce, string address, string signature)
        {
            try
            {
                var signer = new EthereumMessageSigner();
                var signerAddress = signer.EncodeUTF8AndEcRecover(nonce, signature);
                if (signerAddress.ToLower() != address.ToLower())
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // TODO: 設定TOKEN
        public static string jwtLogin(string name, string email, string address, string key, string issuer, string audience)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, name),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim("address", address)
                };

            //取出appsettings.json裡的KEY處理
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            //設定jwt相關資訊
            var jwt = new JwtSecurityToken
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}