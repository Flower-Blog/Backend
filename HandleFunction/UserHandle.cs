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
            if (BackgroundPhoto) return "wwwroot//BackgroundPhoto//" + username;
            return "wwwroot//Picture//" + username;
        }
        public string GetUserImageFileType(string url)
        {
            string file_type = "";
            if (url != null)
            {
                int lastIndex = url.LastIndexOf(".");
                if (lastIndex != -1)
                {
                    file_type = url.Substring(lastIndex + 1);
                    return file_type;
                }
            }
            return file_type;
        }
    }
}