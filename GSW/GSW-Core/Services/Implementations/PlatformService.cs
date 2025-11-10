using GSW_Core.DTOs.Platform;
using GSW_Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class PlatformService : IPlatformService
    {
        public Task<ICollection<PlatformDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
