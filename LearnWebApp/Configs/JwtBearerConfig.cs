using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LearnWebApp.Configs
{
    public static class JwtBearerConfig
    {
        public const string Issuer = "LearnWebServer";
        public const string Audience = "LearnWebServerUser";
        public const string SecretKey = "SECRETsecretSECRETsecretSECRETse";
        public static SymmetricSecurityKey GetKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        }
            
    }
}
