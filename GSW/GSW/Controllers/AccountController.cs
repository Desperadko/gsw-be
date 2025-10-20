using GSW_Core.DTOs.Account;
using GSW_Core.Requests;
using GSW_Core.Responses;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<ActionResult<AccountDTO>> Get([FromQuery]string username)
        {
            var result = await accountService.Get(username);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody]RegisterRequest request)
        {
            var (id, dto) = await accountService.Register(request);

            var accessToken = jwtService.GenerateAccessToken(id, dto);
            var refreshToken = jwtService.GenerateRefreshToken(id);

            await refreshTokenService.Add(id, refreshToken);

            return Ok(new RegisterResponse { Account = dto, Token = accessToken });
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody]LoginRequest request)
        {
            var (id, dto) = await accountService.Login(request);

            var accessToken = jwtService.GenerateAccessToken(id, dto);
            var refreshToken = jwtService.GenerateRefreshToken(id);

            await refreshTokenService.Add(id, refreshToken);

            return Ok(new RegisterResponse { Account = dto, Token = accessToken });
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<RefreshResponse>> Refresh([FromBody]RefreshRequest request)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<LogoutResponse>> Logout([FromBody]LogoutRequest request)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpPut("{id}/role")]
        public async Task<ActionResult<UpdateRoleReponse>> UpdateRole(int id, [FromBody]UpdateRoleRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
