using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Repositories.Interfaces
{
    public interface IPlatformRepository
    {
        Task<Platform?> GetByIdAsync(int id);
        Task<Platform?> GetByNameAsync(string name);
        Task<IEnumerable<Platform>?> GetAllAsync();
        Task<int> AddAsync(Platform platform);
        Task<bool> ExistsByNameAsync(string name);
    }
}
