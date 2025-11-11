using GSW_Core.DTOs.Genre;
using GSW_Core.Requests.Genre;
using GSW_Core.Responses.General;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        [Authorize(Roles = RoleHelper.Admin)]
        public async Task<ActionResult<GetAllResponse<GenreDTO>>> GetAll()
        {
            var genres = await genreService.GetAllAsync();

            return Ok(new GetAllResponse<GenreDTO>(genres));
        }

        [HttpPost]
        [Authorize(Roles = RoleHelper.Admin)]
        public async Task<ActionResult<AddResponse<GenreDTO>>> Add([FromBody]AddGenreRequest request)
        {
            var genre = await genreService.AddAsync(request.Genre);

            return Ok(new AddResponse<GenreDTO>(genre));
        }
    }
}
