using GSW_Core.DTOs.Platform;
using GSW_Core.DTOs.Publisher;
using GSW_Core.Requests.Platform;
using GSW_Core.Requests.Publisher;
using GSW_Core.Responses.General;
using GSW_Core.Services.Implementations;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<GetAllResponse<PublisherDTO>>> GetAll()
        {
            var publishers = await publisherService.GetAllAsync();

            return Ok(new GetAllResponse<PublisherDTO>(publishers));
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<AddResponse<PublisherDTO>>> Add(AddPublisherRequest request)
        {
            var publisher = await publisherService.AddAsync(request.Publisher);

            return Ok(new AddResponse<PublisherDTO>(publisher));
        }
    }
}
