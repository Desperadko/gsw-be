using GSW_Core.DTOs.Platform;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Errors.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository platformRepository;

        public PlatformService(IPlatformRepository platformRepository)
        {
            this.platformRepository = platformRepository;
        }

        public async Task<IEnumerable<PlatformDTO>> GetAllAsync()
        {
            var platforms = await platformRepository.GetAllAsync() ?? throw new NotFoundException("No platforms have been registered");

            return platforms
                .Select(p => new PlatformDTO(p.Name))
                .ToList();
        }
    }
}
