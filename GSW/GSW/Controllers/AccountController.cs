using GSW_Core.DTOs.Account;
using GSW_Core.Requests.Account;
using GSW_Core.Responses.Account;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IJwtService jwtService;
        private readonly IRefreshTokenService refreshTokenService;

        public AccountController(
            IAccountService accountService,
            IJwtService jwtService,
            IRefreshTokenService refreshTokenService)
        {
            this.accountService = accountService;
            this.jwtService = jwtService;
            this.refreshTokenService = refreshTokenService;
        }

        [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<AccountDTO>> Get(string username)
        {
            var result = await accountService.GetAsync(username);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public ActionResult<AccountDTO> GetCurrent()
        {
            var result = accountService.GetCurrent(User.Claims);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody]RegisterRequest request)
        {
            var (id, dto) = await accountService.RegisterAsync(request.Credentials);

            var accessToken = jwtService.GenerateAccessToken(id, dto);
            var refreshToken = jwtService.GenerateRefreshToken(id);

            await refreshTokenService.AddAsync(id, refreshToken);

            return Ok(new RegisterResponse(accessToken, dto));
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody]LoginRequest request)
        {
            var (id, dto) = await accountService.LoginAsync(request.Credentials);

            var accessToken = jwtService.GenerateAccessToken(id, dto);
            var refreshToken = jwtService.GenerateRefreshToken(id);

            await refreshTokenService.AddAsync(id, refreshToken);

            return Ok(new RegisterResponse(accessToken, dto));
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<RefreshResponse>> Refresh([FromBody]RefreshRequest request)
        {
            //validate if access token uses same structure
            var principal = jwtService.ValidateAccessTokenStructure(request.Token);

            if(int.TryParse(principal.FindFirstValue(ClaimTypes.NameIdentifier), out int accountId))
            {
                //validate if last refresh token is revoked or expired
                await refreshTokenService.ValidateLastAsync(accountId);

                var dto = await accountService.GetAsync(accountId);

                var token = jwtService.GenerateAccessToken(accountId, dto);

                return Ok(new RefreshResponse(token));
            }

            return Unauthorized(request);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<LogoutResponse>> Logout()
        {
            if(int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int accountId))
            {
                await refreshTokenService.RevokeLastAsync(accountId);

                return Ok(new LogoutResponse("Logged out successfully"));
            }

            return Unauthorized(new LogoutResponse("Couldn't logout"));
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpPut("{id}/role")]
        public async Task<ActionResult<UpdateRoleReponse>> UpdateRole(int id, [FromBody]UpdateRoleRequest request)
        {
            await refreshTokenService.RevokeAllAsync(id);

            var account = await accountService.UpdateRoleAsync(id, request.Role);

            return Ok(new UpdateRoleReponse(account));
        }
    }
}
