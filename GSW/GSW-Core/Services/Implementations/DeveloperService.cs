using GSW_Core.DTOs.Developer;
using GSW_Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class DeveloperService : IDeveloperService
    {
        public Task<ICollection<DeveloperDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
