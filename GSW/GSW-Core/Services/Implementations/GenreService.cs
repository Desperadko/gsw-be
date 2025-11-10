using GSW_Core.DTOs.Genre;
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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreDTO>> GetAllAsync()
        {
            var genres = await genreRepository.GetAllAsync() ?? throw new NotFoundException("No genres have been registered");

            return genres
                .Select(g => new GenreDTO(g.Name))
                .ToList();
        }
    }
}
