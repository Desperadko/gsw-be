using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.DTOs.Image
{
    public record ImageContentDTO(byte[] Bytes, string ContentType) : BaseDTO
    {
    }
}
