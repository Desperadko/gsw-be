using GSW.Constants;
using GSW_Core.DTOs.Image;
using GSW_Core.Responses.General;
using GSW_Core.Services.Interfaces;
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

        [HttpGet("{fileName}")]
        public async Task<FileResult> Get(string fileName)
        {
            var image = await imageService.GetAsync(fileName);

            return File(image.Bytes, image.ContentType);
        }
    }
}
