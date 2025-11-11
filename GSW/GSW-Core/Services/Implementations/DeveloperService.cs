using GSW_Core.DTOs.Developer;
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
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository developerRepository;

        public DeveloperService(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }

        public async Task<DeveloperDTO> AddAsync(DeveloperAddDTO developerDTO)
        {
            var exists = await developerRepository.ExistsByNameAsync(developerDTO.Name);
            if (exists) throw new BadRequestException($"Developer: '{developerDTO.Name}' already exists");

            var developer = new Developer()
            {
                Name = developerDTO.Name,
            };

            var count = await developerRepository.AddAsync(developer);
            if (count <= 0) throw new BadRequestException("Couldn't add developer to database");

            return new DeveloperDTO(developer.Id, developer.Name);
        }

        public async Task<IEnumerable<DeveloperDTO>> GetAllAsync()
        {
            var developers = await developerRepository.GetAllAsync() ?? throw new NotFoundException("No developers have been registered");

            return developers
                .Select(d => new DeveloperDTO(d.Id, d.Name))
                .ToList();
        }
    }
}
