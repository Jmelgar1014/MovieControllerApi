using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieControllerApi.Models;
using MovieControllerApi.Services;

namespace MovieControllerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly AppDbContext _context;
    
    private readonly TmdbService _tmdbService;

    public MovieController(AppDbContext appDbContext, TmdbService tmdbService)
    {
        _context = appDbContext;

        _tmdbService = tmdbService;
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync(); 

        return movie;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        var movies = await _context.Movies.ToListAsync();

        foreach(var movie in movies)
        {
            movie.PosterPath = await _tmdbService.SearchByTitle(movie.Title);
        }

        return Ok(movies);


    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if(movie == null) return NotFound();

        return movie;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? format, [FromQuery] string? genre)
    {
        var query =  _context.Movies.AsQueryable();
        
        if(!string.IsNullOrEmpty(title))
        {
            query = query.Where(movie => movie.Title.Contains(title));
        }
        if(!string.IsNullOrEmpty(format))
        {
            if(!Enum.TryParse<FormatType>(format, true, out var fmt))
            {
                return BadRequest($"Unknown format: {format}");
            }
            query = query.Where(movie => movie.Format.Contains(fmt));
        }
        if(!string.IsNullOrEmpty(genre))
        {
            query = query.Where(movie => movie.Genre.Contains(genre));
        }

        var results = await query.ToListAsync();

        return Ok(results);

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Movie>> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if(movie == null)
        {
            return NotFound();          
        }

        _context.Movies.Remove(movie);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMovie(int id, [FromBody] Movie newMovie)
    {
        if(id != newMovie.Id) return BadRequest();

        var movie = await _context.Movies.FindAsync(id);

        if(movie == null) return NotFound();

        movie.Title = newMovie.Title;
        movie.Format = newMovie.Format;
        movie.Genre = newMovie.Genre;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}/watched")]
    public async Task<ActionResult> UpdateWatched(int id, [FromBody] bool watched)
    {
        var movie = await _context.Movies.FindAsync(id);

        if(movie == null) return NotFound();

        movie.WatchedMovie = watched;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
