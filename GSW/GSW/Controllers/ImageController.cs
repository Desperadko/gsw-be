using GSW.Constants;
using GSW_Core.DTOs.Image;
using GSW_Core.Requests.Image;
using GSW_Core.Responses.General;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GSW.Controllers
{
    [Route(ApiRoutes.ImageController)]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet]
        public async Task<FileResult> Get([FromQuery]string fileName)
        {
            var image = await imageService.GetAsync(fileName);

            return File(image.Bytes, image.ContentType);
        }

        [HttpPost]
        [Authorize(Roles = RoleHelper.Admin)]
        public async Task<ActionResult<AddResponse<ImageMetaDTO>>> Add([FromForm]AddImageRequest request)
        {
            var image = await imageService.AddAsync(request.Image);

            var url = Path.Combine(ApiRoutes.ImageController, image.FileName);
            image.URL = url;

            return Ok(new AddResponse<ImageMetaDTO>(image));
        }

        [HttpPost("default")]
        [Authorize(Roles = RoleHelper.Admin)]
        public async Task<ActionResult<AddResponse<ImageMetaDTO>>> AddDefault([FromForm]AddDefaultImageRequest request)
        {
            var image = await imageService.AddDefaultAsync(request.ProductId);

            var url = Path.Combine(ApiRoutes.ImageController, image.FileName);
            image.URL = url;

            return Ok(new AddResponse<ImageMetaDTO>(image));
        }
    }
}
