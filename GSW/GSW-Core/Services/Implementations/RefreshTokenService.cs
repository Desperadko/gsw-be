using GSW_Core.Repositories.Interfaces;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
using GSW_Core.Utilities.Errors.Exceptions;
using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<string> AddAsync(int accountId, string token, DateTime? expiresAt = null)
        {
            var refreshToken = new RefreshToken
            {
                AccountId = accountId,
                Token = token,
                ExpiresAt = expiresAt ?? DateTime.UtcNow.AddDays(JWTConstants.REFRESH_TOKEN_EXPIRATION_DAYS),
            };

            var count = await refreshTokenRepository.AddAsync(refreshToken);
            if (count <= 0) throw new BadRequestException("Couln't add new Refresh Token.");

            return refreshToken.Token;
        }

        public async Task<bool> ValidateAsync(string token)
        {
            var isValid = await refreshTokenRepository.IsValidAsync(token);
            if (!isValid) throw new UnauthorizedException("Refresh token is not valid.");

            return isValid;
        }

        public async Task<bool> ValidateLastAsync(int accountId)
        {
            var isValid = await refreshTokenRepository.IsValidAsync(accountId);
            if (!isValid) throw new UnauthorizedException("Last refresh token of this account is not valid.");
            
            return isValid;
        }

        public async Task RevokeAsync(string token)
        {
            var refreshToken = await refreshTokenRepository.GetAsync(token) ?? throw new NotFoundException("Refresh token not found.");

            refreshToken.IsRevoked = true;

            var count = await refreshTokenRepository.SaveAsync();
            if (count <= 0) throw new BadRequestException("Couldn't revoke token.");
        }

        public async Task RevokeLastAsync(int accountId)
        {
            var refreshToken = await refreshTokenRepository.GetLastAsync(accountId) ?? throw new NotFoundException($"No refresh token found for account with id: '{accountId}'");

            refreshToken.IsRevoked = true;

            var count = await refreshTokenRepository.SaveAsync();
            if (count <= 0) throw new BadRequestException("Couldn't revoke token.");
        }

        public async Task RevokeAllAsync(int accountId)
        {
            var unrevokedTokens = await refreshTokenRepository.GetAllValidByAccountIdAsync(accountId) ?? throw new NotFoundException("No Refresh Tokens found for account.");

            if (!unrevokedTokens.Any()) return;

            foreach(var token in unrevokedTokens)
            {
                token.IsRevoked = true;
            }

            var count = await refreshTokenRepository.SaveAsync();
            if (count < unrevokedTokens.Count()) throw new BadRequestException($"Couldn't revoke Refresh Tokens. {count} out of {unrevokedTokens.Count()} revoked.");
        }
    }
}
