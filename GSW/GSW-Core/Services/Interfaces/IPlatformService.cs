using GSW_Core.DTOs.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformDTO>> GetAllAsync();
        Task<PlatformDTO> AddAsync(PlatformAddDTO platformDTO);
    }
}
