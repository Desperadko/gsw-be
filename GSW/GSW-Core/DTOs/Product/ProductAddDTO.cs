using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.DTOs.Product
{
    public record ProductAddDTO(
        string Name,
        string Description,
        DateTime ReleaseDate,
        decimal Price,
        ICollection<int> DevelopersIds,
        ICollection<int> PublishersIds,
        ICollection<int> GenresIds,
        ICollection<int> PlatformsIds)
    {
    }
}
