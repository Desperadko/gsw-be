using GSW_Core.DTOs.Publisher;
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
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

        public async Task<PublisherDTO> AddAsync(PublisherAddDTO publisherDTO)
        {
            var exists = await publisherRepository.ExistsByNameAsync(publisherDTO.Name);
            if (exists) throw new BadRequestException($"Publisher: '{publisherDTO.Name}' already exists");

            var publisher = new Publisher()
            {
                Name = publisherDTO.Name,
            };

            var count = await publisherRepository.AddAsync(publisher);
            if(count <= 0) throw new BadRequestException("Couldn't add publisher to database");

            return new PublisherDTO(publisher.Id, publisher.Name);
        }

        public async Task<IEnumerable<PublisherDTO>> GetAllAsync()
        {
            var publishers = await publisherRepository.GetAllAsync() ?? throw new NotFoundException("No publishers have been registered");

            return publishers
                .Select(p => new PublisherDTO(p.Id, p.Name))
                .ToList();
        }
    }
}
