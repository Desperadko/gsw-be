using GSW.Constants;
using GSW_Core.DTOs.Product;
using GSW_Core.Requests.Product;
using GSW_Core.Responses.General;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IImageService imageService;

        public ProductController(
            IProductService productService,
            IImageService imageService)
        {
            this.productService = productService;
            this.imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<GetResponse<ProductDTO>>> Get([FromQuery]int id)
        {
            var product = await productService.GetAsync(id);

            var imageFileName = imageService.GetFileName(id);
            var imageURL = ApiRoutes.ImageController + "/" + imageFileName;

            var productWithImage = product with { ImageURL = imageURL };

            return Ok(new GetResponse<ProductDTO>(productWithImage));
        }

        [HttpPost]
        [Authorize(Roles = RoleHelper.Admin)]
        public async Task<ActionResult<AddResponse<ProductDTO>>> Add([FromBody]AddProductRequest request)
        {
            var product = await productService.AddAsync(request.Product);

            var imageFileName = await imageService.AddAsync(product.Id, request.Image);
            var imageURL = ApiRoutes.ImageController + "/" + imageFileName;

            var productWithImage = product with { ImageURL = imageURL };

            return Ok(new AddResponse<ProductDTO>(productWithImage));
        }
    }
}
