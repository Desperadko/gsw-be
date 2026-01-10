using GSW_Core.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Requests.Genre
{
    public record AddGenreRequest(GenreAddDTO Genre)
    {
    }
}
