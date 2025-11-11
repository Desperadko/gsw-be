using GSW_Core.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetAllAsync();
        Task<GenreDTO> AddAsync(GenreAddDTO genre);
    }
}
