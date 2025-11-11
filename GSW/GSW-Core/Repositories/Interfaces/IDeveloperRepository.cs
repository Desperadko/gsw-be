using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Repositories.Interfaces
{
    public interface IDeveloperRepository
    {
        Task<Developer?> GetByIdAsync(int id);
        Task<Developer?> GetByNameAsync(string name);
        Task<IEnumerable<Developer>?> GetAllAsync();
        Task<int> AddAsync(Developer developer);
        Task<bool> ExistsByNameAsync(string name);
    }
}
