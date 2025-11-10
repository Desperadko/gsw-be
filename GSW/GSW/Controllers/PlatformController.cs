using GSW_Core.DTOs.Platform;
using GSW_Core.Responses.General;
using GSW_Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService platformService;

        public PlatformController(IPlatformService platformService)
        {
            this.platformService = platformService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetAllResponse<PlatformDTO>>> GetAll()
        {
            var platforms = await platformService.GetAllAsync();

            return Ok(new GetAllResponse<PlatformDTO>(platforms));
        }
    }
}
