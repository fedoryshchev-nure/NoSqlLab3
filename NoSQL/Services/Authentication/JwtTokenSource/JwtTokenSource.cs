using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoSQL.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NoSQL.Services.Authentication.JwtTokenSource
{
    public class JwtTokenSource : IJwtTokenSource
    {
        private readonly JwtOptions options;

        public JwtTokenSource(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }

        public string Get(string id)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, id) };
            var expires = now.Add(TimeSpan.FromDays(options.ExpirationDays));
            var signedKey = new SigningCredentials(options.GetSymmetricSecurityKey,
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                    issuer: options.Issuer,
                    audience: options.Issuer,
                    notBefore: now,
                    expires: expires,
                    claims: claims,
                    signingCredentials: signedKey);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}
