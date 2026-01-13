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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetResponse<ProductDTO>>> Get([FromRoute]int id)
        {
            var product = await productService.GetAsync(id);

            var imageFileName = imageService.GetFileName(id);
            var imageURL = ApiRoutes.ImageController + "/" + imageFileName;

            var productWithImage = product with { ImageURL = imageURL };

            return Ok(new GetResponse<ProductDTO>(productWithImage));
        }

        [HttpGet]
        public async Task<ActionResult<GetAllResponse<ProductDTO>>> Get([FromQuery]GetProductsRequest request)
        {
            var products = await productService.GetAllAsync(request.Filter, request.Sort, request.Pagination);

            var productsWithImages = new List<ProductDTO>();

            foreach (var product in products)
            {
                var imageFileName = imageService.GetFileName(product.Id);
                var imageURL = ApiRoutes.ImageController + "/" + imageFileName;

                var productWithImage = product with { ImageURL = imageURL };

                productsWithImages.Add(productWithImage);
            }

            return Ok(new GetAllResponse<ProductDTO>(productsWithImages));
        }

        [HttpPost]
        [Authorize(Roles = RoleHelper.Admin)]
        public async Task<ActionResult<AddResponse<ProductDTO>>> Add([FromBody]AddProductRequest request)
        {
            var product = await productService.AddAsync(request.Product);

            return Ok(new AddResponse<ProductDTO>(product));
        }
    }
}
