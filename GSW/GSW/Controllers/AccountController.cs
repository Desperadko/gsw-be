using GSW_Core.DTOs.Account;
using GSW_Core.Requests;
using GSW_Core.Responses;
using GSW_Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AccountDTO>> Get([FromQuery]string username)
        {
            var result = await accountService.Get(username);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody]RegisterRequest request)
        {
            var response = await accountService.Register(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody]LoginRequest request)
        {
            var response = await accountService.Login(request);
            return Ok(response);
        }
    }
}
