using GSW_Core.DTOs.Genre;
using GSW_Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class GenreService : IGenreService
    {
        public Task<ICollection<GenreDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
