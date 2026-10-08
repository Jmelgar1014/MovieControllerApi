using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieControllerApi.Services;

namespace MovieControllerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmdbController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TmdbController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("images")]
        public async Task<IActionResult> GetMovieImagesPerPage([FromBody]List<string> movieTitles, [FromServices] TmdbService tmdb)
        {
           var results = new Dictionary<string, string?>();

           foreach(var title in movieTitles)
            {
                results[title] = await tmdb.SearchByTitle(title);
            } 
            return Ok(results);
        }
    }
}
