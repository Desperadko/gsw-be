using GSW_Core.DTOs.Product;
using GSW_Core.Responses.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Responses.Product
{
    public record GetProductResponse : GetResponse<ProductDTO>
    {
        public GetProductResponse(ProductDTO DTO) : base(DTO)
        {
        }
    }
}
