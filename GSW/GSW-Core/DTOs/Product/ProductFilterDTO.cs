using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.DTOs.Product
{
    public record ProductFilterDTO(
        IEnumerable<int>? DevelopersIds,
        IEnumerable<int>? PublishersIds,
        IEnumerable<int>? GenresIds,
        IEnumerable<int>? PlatformsIds,
        
        int? MinPrice,
        int? MaxPrice)
    {
    }
}
