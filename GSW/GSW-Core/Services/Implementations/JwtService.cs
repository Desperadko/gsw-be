using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
using GSW_Data.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly string jwtKey;
        private readonly string jwtIssuer;
        private readonly string jwtAudience;

        public JwtService()
        {
            jwtKey = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.JWT_KEY) ?? throw new InvalidOperationException("JWT Key not set.");
            jwtIssuer = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.JWT_ISSUER) ?? throw new InvalidOperationException("JWT Issuer not set.");
            jwtAudience = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.JWT_AUDIENCE) ?? throw new InvalidOperationException("JWT Audience not set.");
        }

        public string GenerateToken(Account account)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new(ClaimTypes.Name, account.Username),
                new(ClaimTypes.Email, account.Email),
                new(ClaimTypes.Role, account.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
