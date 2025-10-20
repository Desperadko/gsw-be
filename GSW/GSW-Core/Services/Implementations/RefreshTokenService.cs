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
        private readonly IAccountRepository accountRepository;

        public RefreshTokenService(
            IRefreshTokenRepository refreshTokenRepository,
            IAccountRepository accountRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
            this.accountRepository = accountRepository;
        }

        public async Task<string> Add(int accountId, string token, DateTime? expiresAt = null)
        {
            var refreshToken = new RefreshToken
            {
                AccountId = accountId,
                Token = token,
                ExpiresAt = expiresAt ?? DateTime.UtcNow.AddDays(JWTConstants.REFRESH_TOKEN_EXPIRATION_DAYS),
            };

            var count = await refreshTokenRepository.Add(refreshToken);
            if (count <= 0) throw new BadRequestException("Couln't add new Refresh Token.");

            return refreshToken.Token;
        }

        public async Task<bool> Validate(string token)
        {
            return await refreshTokenRepository.IsValid(token);
        }

        public async Task Revoke(string token)
        {
            var refreshToken = await refreshTokenRepository.Get(token) ?? throw new NotFoundException("Refresh token not found.");

            refreshToken.IsRevoked = true;

            var count = await refreshTokenRepository.Save();
            if (count <= 0) throw new BadRequestException("Couldn't revoke token.");
        }

        public async Task RevokeAll(int accountId)
        {
            var refreshTokens = await refreshTokenRepository.GetAllByAccountId(accountId) ?? throw new NotFoundException("No Refresh Tokens found for account.");

            foreach(var token in refreshTokens)
            {
                token.IsRevoked = true;
            }

            var count = await refreshTokenRepository.Save();
            if (count < refreshTokens.Count()) throw new BadRequestException($"Couldn't revoke Refresh Tokens. {count} out of {refreshTokens.Count()} revoked.");
        }
    }
}
