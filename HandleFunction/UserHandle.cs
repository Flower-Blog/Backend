using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Nethereum.Signer;

namespace DotnetWebApi.HandleFunction
{
    class UserHandle
    {
        public string GetFilePath(string username, bool BackgroundPhoto)
        {
            if (BackgroundPhoto) return ".\\wwwroot\\BackgroundPhoto\\" + username;
            return ".\\wwwroot\\Picture\\" + username;
        }
    }
}