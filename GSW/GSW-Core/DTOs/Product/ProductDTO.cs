using GSW_Core.DTOs.Developer;
using GSW_Core.DTOs.Genre;
using GSW_Core.DTOs.Platform;
using GSW_Core.DTOs.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.DTOs.Product
{
    public record ProductDTO(
        string Name,
        string Description,
        DateTime ReleaseDate,
        decimal Price,
        ICollection<DeveloperDTO> Developers,
        ICollection<PublisherDTO> Publishers,
        ICollection<GenreDTO> Genres,
        ICollection<PlatformDTO> Platforms)
    {
    }
}
