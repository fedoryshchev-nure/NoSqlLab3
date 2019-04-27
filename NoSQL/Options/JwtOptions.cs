using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NoSQL.Configurations
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpirationDays { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey
        {
            get => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }
    }
}
