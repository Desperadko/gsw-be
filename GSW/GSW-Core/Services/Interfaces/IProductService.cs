using GSW_Core.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> Get(int id);
        Task<ProductDTO> Add(ProductDTO product)
    }
}
