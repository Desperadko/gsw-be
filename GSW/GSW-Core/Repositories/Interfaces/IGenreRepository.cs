using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<Genre?> GetAsync(int id);
        Task<IEnumerable<Genre>?> GetAllAsync();
        Task<int> AddAsync(Genre genre);
    }
}
