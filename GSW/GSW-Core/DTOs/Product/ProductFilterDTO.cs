using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.DTOs.Product
{
    public record ProductFilterDTO
    {
        public IEnumerable<int>? DevelopersIds { get; set; }
        public IEnumerable<int>? PublishersIds { get; set; }
        public IEnumerable<int>? GenresIds { get; set; }
        public IEnumerable<int>? PlatformsIds { get; set; }
        
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
