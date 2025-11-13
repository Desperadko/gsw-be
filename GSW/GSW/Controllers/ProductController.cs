using GSW_Core.Requests.Product;
using GSW_Core.Responses.General;
using GSW_Core.Responses.Product;
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
        public async Task<ActionResult<GetProductResponse>> Get([FromQuery]int id)
        {
            var product = await productService.GetAsync(id);

            return Ok(new GetProductResponse(product));
        }

        [HttpPost]
        [Authorize(Roles = RoleHelper.Admin)]
        public async Task<ActionResult<AddProductResponse>> Add([FromBody]AddProductRequest request)
        {
            var product = await productService.AddAsync(request.Product);

            return Ok(new AddProductResponse(product));
        } 
    }
}
