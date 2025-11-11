using GSW_Core.DTOs.Developer;
using GSW_Core.Requests.Developer;
using GSW_Core.Responses.General;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
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
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<GetAllResponse<DeveloperDTO>>> GetAll()
        {
            var developers = await developerService.GetAllAsync();

            return Ok(new GetAllResponse<DeveloperDTO>(developers));
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<AddResponse<DeveloperDTO>>> Add([FromBody]AddDeveloperRequest request)
        {
            var developer = await developerService.AddAsync(request.Developer);

            return Ok(new AddResponse<DeveloperDTO>(developer));
        }
    }
}
