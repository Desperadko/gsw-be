using GSW_Core.DTOs.Publisher;
using GSW_Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class PublisherService : IPublisherService
    {
        public Task<ICollection<PublisherDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
