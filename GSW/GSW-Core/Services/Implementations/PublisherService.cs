using GSW_Core.DTOs.Publisher;
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
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

        public Task<PublisherDTO> AddAsync(PublisherAddDTO publisherDTO)
        {
            throw new NotImplementedException();
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
