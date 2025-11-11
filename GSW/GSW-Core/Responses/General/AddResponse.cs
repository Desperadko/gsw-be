using GSW_Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Responses.General
{
    public record AddResponse<T>(T DTO) where T : BaseDTO
    {
    }
}
