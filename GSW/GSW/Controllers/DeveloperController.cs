using GSW_Core.DTOs.Developer;
using GSW_Core.Responses.General;
using GSW_Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            this.developerService = developerService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetAllResponse<DeveloperDTO>>> GetAll()
        {
            var developers = await developerService.GetAllAsync();

            return Ok(new GetAllResponse<DeveloperDTO>(developers));
        }
    }
}
