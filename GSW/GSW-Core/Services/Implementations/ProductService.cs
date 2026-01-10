using GSW_Core.Cachables;
using GSW_Core.DTOs;
using GSW_Core.DTOs.Developer;
using GSW_Core.DTOs.Genre;
using GSW_Core.DTOs.Platform;
using GSW_Core.DTOs.Product;
using GSW_Core.DTOs.Publisher;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Errors.Exceptions;
using GSW_Data.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Immutable;

namespace GSW_Core.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        private readonly IDeveloperRepository developerRepository;
        private readonly IPublisherRepository publisherRepository;
        private readonly IGenreRepository genreRepository;
        private readonly IPlatformRepository platformRepository;

        private readonly IMemoryCache cache;

        private const string COMPLEMENTARY_ENTITIES_CACHE_KEY = "complementary_entities_cache_key";

        public ProductService(
            IProductRepository productRepository,
            IDeveloperRepository developerRepository,
            IPublisherRepository publisherRepository,
            IGenreRepository genreRepository,
            IPlatformRepository platformRepository,
            IMemoryCache cache)
        {
            this.productRepository = productRepository;
            this.developerRepository = developerRepository;
            this.publisherRepository = publisherRepository;
            this.genreRepository = genreRepository;
            this.platformRepository = platformRepository;
            this.cache = cache;
        }

        public async Task<ProductDTO> AddAsync(ProductAddDTO productDTO)
        {
            var exists = await productRepository.ExistsByNameAsync(productDTO.Name);
            if (exists) throw new BadRequestException($"Product with this name already exists: '{productDTO.Name}'");

            //get all complementary entities as dtos
            var complementaryDTOs = await GetComplementaryEntitiesAsync();

            //check if complementary entities exist
            ValidateComplementaryEntities(productDTO, complementaryDTOs);

            //filter complementary entities
            //  and convert them to their model object types
            var (filteredDevelopers, filteredDevelopersDTOs) = FilterAndConvert<Developer, DeveloperDTO>(
                productDTO.DevelopersIds,
                complementaryDTOs.Developers,
                d => d.Id,
                d => new Developer() { Id = d.Id, Name = d.Name });

            var (filteredPublishers, filteredPublishersDTOs) = FilterAndConvert<Publisher, PublisherDTO>(
                productDTO.PublishersIds,
                complementaryDTOs.Publishers,
                p => p.Id,
                p => new Publisher() { Id = p.Id, Name = p.Name });

            var (filteredGenres, filteredGenresDTOs) = FilterAndConvert<Genre, GenreDTO>(
                productDTO.GenresIds,
                complementaryDTOs.Genres,
                g => g.Id,
                g => new Genre() { Id = g.Id, Name = g.Name });

            var (filteredPlatforms, filteredPlatformsDTOs) = FilterAndConvert<Platform, PlatformDTO>(
                productDTO.PlatformsIds,
                complementaryDTOs.Platforms,
                p => p.Id,
                p => new Platform() { Id = p.Id, Name= p.Name });

            var product = new Product()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                ReleaseDate = productDTO.ReleaseDate,
                Price = productDTO.Price,
                Developers = filteredDevelopers,
                Publishers = filteredPublishers,
                Genres = filteredGenres,
                Platforms = filteredPlatforms,
            };

            //the add method in the repository makes sure
            //  the 'new' complementary entities are not actually new
            //  so no duplicate entities are assigned
            var count = await productRepository.AddAsync(product);
            if (count <= 0) throw new BadRequestException("Couldn't add product");

            return new ProductDTO(
                product.Id,
                product.Name,
                product.Description,
                product.ReleaseDate,
                product.Price,
                filteredDevelopersDTOs,
                filteredPublishersDTOs,
                filteredGenresDTOs,
                filteredPlatformsDTOs);
        }

        public async Task<ProductDTO> GetAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Product with Id: '{id}' doesn't exist");

            var developerDTOs = product.Developers?.Select(d => new DeveloperDTO(d.Id, d.Name)).ToList()
                ?? throw new NotFoundException("Product's developers were not loaded from the database");

            var publisherDTOs = product.Publishers?.Select(p => new PublisherDTO(p.Id, p.Name)).ToList()
                ?? throw new NotFoundException("Product's publishers were not loaded from the database");

            var genreDTOs = product.Genres?.Select(d => new GenreDTO(d.Id, d.Name)).ToList()
                ?? throw new NotFoundException("Product's genres were not loaded from the database");

            var platformDTOs = product.Platforms?.Select(p => new PlatformDTO(p.Id, p.Name)).ToList()
                ?? throw new NotFoundException("Product's platforms were not loaded from the database");

            return new ProductDTO(
                product.Id,
                product.Name,
                product.Description,
                product.ReleaseDate,
                product.Price,
                developerDTOs,
                publisherDTOs,
                genreDTOs,
                platformDTOs);
        }

        private async Task<ProductComplementaryEntitiesDTOs> GetComplementaryEntitiesAsync()
        {
            if(cache.TryGetValue(COMPLEMENTARY_ENTITIES_CACHE_KEY, out ProductComplementaryEntitiesDTOs? dtos) && dtos != null)
            {
                return dtos;
            }
            else
            {
                var developers = await developerRepository.GetAllAsync() ?? throw new NotFoundException("No developers found to fetch");
                var publishers = await publisherRepository.GetAllAsync() ?? throw new NotFoundException("No publishers found to fetch");
                var genres = await genreRepository.GetAllAsync() ?? throw new NotFoundException("No genres found to fetch");
                var platforms = await platformRepository.GetAllAsync() ?? throw new NotFoundException("No platforms found to fetch");

                dtos = new ProductComplementaryEntitiesDTOs(
                    developers.Select(d => new DeveloperDTO(d.Id, d.Name)).ToImmutableHashSet(),
                    publishers.Select(p => new PublisherDTO(p.Id, p.Name)).ToImmutableHashSet(),
                    genres.Select(g => new GenreDTO(g.Id, g.Name)).ToImmutableHashSet(),
                    platforms.Select(p => new PlatformDTO(p.Id, p.Name)).ToImmutableHashSet());

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                cache.Set(COMPLEMENTARY_ENTITIES_CACHE_KEY, dtos, cacheEntryOptions);

                return dtos;
            }
        }

        private void ValidateComplementaryEntities(ProductAddDTO dto, ProductComplementaryEntitiesDTOs complementaryEntities)
        {
            var errors = new List<string>();

            var existingDeveloperIds = complementaryEntities.Developers.Select(d => d.Id).ToImmutableHashSet();
            var existingPublisherIds = complementaryEntities.Publishers.Select(p => p.Id).ToImmutableHashSet();
            var existingGenreIds = complementaryEntities.Genres.Select(g => g.Id).ToImmutableHashSet();
            var existingPlatformIds = complementaryEntities.Platforms.Select(p => p.Id).ToImmutableHashSet();

            var invalidDevelopers = dto.DevelopersIds.Where(id => !existingDeveloperIds.Contains(id));
            var invalidPublishers = dto.PublishersIds.Where(id => !existingPublisherIds.Contains(id));
            var invalidGenres = dto.GenresIds.Where(id => !existingGenreIds.Contains(id));
            var invalidPlatforms = dto.PlatformsIds.Where(id => !existingPlatformIds.Contains(id));

            if (invalidDevelopers.Any()) errors.Add($"Invalid developers: {string.Join(", ", invalidDevelopers)}");
            if (invalidPublishers.Any()) errors.Add($"Invalid publishers: {string.Join(", ", invalidPublishers)}");
            if (invalidGenres.Any()) errors.Add($"Invalid genres: {string.Join(", ", invalidGenres)}");
            if (invalidPlatforms.Any()) errors.Add($"Invalid platforms: {string.Join(", ", invalidPlatforms)}");

            if (errors.Count != 0) throw new BadRequestException(string.Join("; ", errors));
        }

        //helper method, only for the 'AddAsync' method to be more readable
        private (ICollection<TModel> models, ICollection<TDTO> dtos) FilterAndConvert<TModel, TDTO>(
            ICollection<int> ids,
            ICollection<TDTO> allDTOs,
            Func<TDTO, int> idSelector,
            Func<TDTO, TModel> modelConverter)
            where TModel : class
            where TDTO : class
        {
            var filteredDTOs = allDTOs
                .Where(dto => ids.Contains(idSelector(dto)))
                .ToList();

            var filteredModels = filteredDTOs
                .Select(modelConverter)
                .ToList();

            return (filteredModels, filteredDTOs);
        }
    }
}
