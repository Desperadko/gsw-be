using GSW_Core.DTOs.Platform;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Errors.Exceptions;
using GSW_Data.Models;
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

        public async Task<PlatformDTO> AddAsync(PlatformAddDTO platformDTO)
        {
            var platform = new Platform()
            {
                Name = platformDTO.Name,
            };

            var count = await platformRepository.AddAsync(platform);
            if (count <= 0) throw new BadRequestException("Couldn't add platform to database");

            return new PlatformDTO(platform.Id, platform.Name);
        }

        public async Task<IEnumerable<PlatformDTO>> GetAllAsync()
        {
            var platforms = await platformRepository.GetAllAsync() ?? throw new NotFoundException("No platforms have been registered");

            return platforms
                .Select(p => new PlatformDTO(p.Id, p.Name))
                .ToList();
        }
    }
}
