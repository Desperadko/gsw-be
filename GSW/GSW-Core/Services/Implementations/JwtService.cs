using GSW_Core.DTOs.Account;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
using GSW_Core.Utilities.Errors.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GSW_Core.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly string jwtKey;
        private readonly string jwtIssuer;
        private readonly string jwtAudience;

        private const string tokenTypeClaimName = "token_type";

        public JwtService()
        {
            jwtKey = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.JWT_KEY) ?? throw new InvalidOperationException("JWT Key not set.");
            jwtIssuer = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.JWT_ISSUER) ?? throw new InvalidOperationException("JWT Issuer not set.");
            jwtAudience = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.JWT_AUDIENCE) ?? throw new InvalidOperationException("JWT Audience not set.");
        }

        public string GenerateAccessToken(int accountId, AccountDTO accountDTO)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, accountId.ToString()),
                new(ClaimTypes.Name, accountDTO.Username),
                new(ClaimTypes.Email, accountDTO.Email),
                new(ClaimTypes.Role, accountDTO.Role),
                new(tokenTypeClaimName, "access")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(JWTConstants.ACCESS_TOKEN_EXPIRATION_HOURS),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(int accountId)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, accountId.ToString()),
                new(tokenTypeClaimName, "refresh")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(JWTConstants.REFRESH_TOKEN_EXPIRATION_DAYS),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateRefreshTokenStructure(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = jwtAudience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out _) ?? throw new UnauthorizedException($"Invalid token: '{token}'");

                var tokenType = principal.FindFirst(tokenTypeClaimName)?.Value;
                if (tokenType == null || tokenType == "refresh") throw new UnauthorizedException($"Invalid token type: '{tokenType}'");

                return principal;
            }
            catch (Exception ex)
            {
                //It looks wrong to throw a new exception after catching one,
                //  but the ExceptionFilter takes care of this one.
                //  When this is thrown, an ExceptionResponse is constructed and sent to the FE
                //  with the respective error status code
                throw new BadRequestException($"Something went wrong while validating JWT token structure. {ex}");
            }
        }
    }
}
