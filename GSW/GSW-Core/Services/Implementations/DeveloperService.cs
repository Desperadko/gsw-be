using GSW_Core.DTOs.Developer;
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
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository developerRepository;

        public DeveloperService(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }

        public async Task<IEnumerable<DeveloperDTO>> GetAllAsync()
        {
            var developers = await developerRepository.GetAllAsync() ?? throw new NotFoundException("No developers have been registered");

            return developers
                .Select(d => new DeveloperDTO(d.Name))
                .ToList();
        }
    }
}
