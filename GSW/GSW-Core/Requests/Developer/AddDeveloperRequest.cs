using GSW_Core.DTOs.Developer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Requests.Developer
{
    public record AddDeveloperRequest(DeveloperAddDTO Developer)
    {
    }
}
