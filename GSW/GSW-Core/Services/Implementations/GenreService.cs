using GSW_Core.DTOs.Genre;
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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task<GenreDTO> AddAsync(GenreAddDTO genreDTO)
        {
            var genre = new Genre()
            {
                Name = genreDTO.Name,
            };

            var count = await genreRepository.AddAsync(genre);
            if (count <= 0) throw new BadRequestException("Couldn't add genre to database");

            return new GenreDTO(genre.Id, genre.Name);
        }

        public async Task<IEnumerable<GenreDTO>> GetAllAsync()
        {
            var genres = await genreRepository.GetAllAsync() ?? throw new NotFoundException("No genres have been registered");

            return genres
                .Select(g => new GenreDTO(g.Id, g.Name))
                .ToList();
        }
    }
}
