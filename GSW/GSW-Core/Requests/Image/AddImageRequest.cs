using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Requests.Image
{
    public record AddImageRequest(int ProductId, IFormFile Image)
    {
    }
}
