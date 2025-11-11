using GSW_Core.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Requests.Product
{
    public record AddProductRequest(ProductAddDTO Product)
    {
    }
}
