using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.DTOs.Developer
{
    public record DeveloperDTO(int Id, string Name) : BaseDTO
    {
    }
}
