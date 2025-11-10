using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Repositories.Interfaces
{
    public interface IPublisherRepository
    {
        Task<Publisher?> GetAsync(int id);
        Task<IEnumerable<Publisher>?> GetAllAsync();
        Task<int> AddAsync(Publisher publisher);
    }
}
