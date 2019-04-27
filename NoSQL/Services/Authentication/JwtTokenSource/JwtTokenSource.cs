using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoSQL.Configurations;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace NoSQL.Services.Authentication.JwtTokenSource
{
    public class JwtTokenSource : IJwtTokenSource
    {
        private readonly JwtOptions options;

        public JwtTokenSource(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }

        public string Token
        {
            get
            {
                var now = DateTime.UtcNow;
                var expires = now.Add(TimeSpan.FromDays(options.ExpirationDays));
                var signedKey = new SigningCredentials(options.GetSymmetricSecurityKey,
                    SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(
                        issuer: options.Issuer,
                        audience: options.Issuer,
                        notBefore: now,
                        expires: expires,
                        signingCredentials: signedKey);

                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                return token;
            }
        }
    }
}
