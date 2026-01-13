using GSW_Core.DTOs;
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
        Task<ProductDTO> GetAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllAsync(ProductFilterDTO? filter, SortDTO? sort, PaginationDTO? pagination);
        Task<ProductDTO> AddAsync(ProductAddDTO productDTO);
    }
}
