using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.DTOs.Image
{
    public record ImageAddDTO(int ProductId, IFormFile ImageData)
    {
    }
}
