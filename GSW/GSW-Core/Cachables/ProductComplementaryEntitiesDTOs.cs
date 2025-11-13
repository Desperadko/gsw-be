using GSW_Core.DTOs.Developer;
using GSW_Core.DTOs.Genre;
using GSW_Core.DTOs.Platform;
using GSW_Core.DTOs.Publisher;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Cachables
{
    internal record ProductComplementaryEntitiesDTOs(
        ICollection<DeveloperDTO> Developers,
        ICollection<PublisherDTO> Publishers,
        ICollection<GenreDTO> Genres,
        ICollection<PlatformDTO> Platforms)
    {
    }
}
