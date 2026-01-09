using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RestAPI.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";//
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
