using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.DTOs
{
    public record PaginationDTO(int Page = 1, int Size = 20)
    {
    }
}
