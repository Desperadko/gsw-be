using GSW_Core.DTOs.Product;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Task<ProductDTO> AddAsync(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
