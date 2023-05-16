using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmsController : ControllerBase
{
    private readonly ILogger<FilmsController> _logger;
    private readonly IFilmService _filmService;

    public FilmsController(ILogger<FilmsController> logger, IFilmService filmService) {
        _logger = logger;
        _filmService = filmService;
    }

    [HttpGet("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<Film>> GetFilmAsync(int id, CancellationToken cancellationToken) {
        var film = await _filmService.GetFilmAsync(id, cancellationToken);

        if (film is null) {
            _logger.LogWarning("Film with id: {FilmId} wasn't found in the database.", id);
            return NotFound();
        }

        return film;
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<IEnumerable<Film>>> GetAllFilmsAsync(CancellationToken cancellationToken) {
        var films = await _filmService.GetAllFilmsAsync(cancellationToken);

        if (films is null || !films.Any()) {
            _logger.LogWarning("There wasn't any films in the database.");
            return NotFound();
        }

        return films.ToList();
        
    }

    [HttpPut("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutFilmAsync(int id, Film film, CancellationToken cancellationToken) {
        if (id != film.Id) {
            _logger.LogWarning("Id's don't match.");
            return BadRequest();
        }

        if (!await _filmService.CheckIfFilmExists(id, cancellationToken)) {
            _logger.LogWarning("Film with id: {FilmId} wasn't found in the database.", id);
            return NotFound();
        }

        await _filmService.UpdateFilmAsync(film, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> PostFilmAsync(Film film, CancellationToken cancellationToken) {
        await _filmService.AddFilmAsync(film, cancellationToken);

        return CreatedAtAction(nameof(PostFilmAsync), new { id = film.Id }, film);
    }

    [HttpDelete("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteFilmAsync(int id, CancellationToken cancellationToken) {
        if (!await _filmService.CheckIfFilmExists(id, cancellationToken)) {
            _logger.LogWarning("Film with id: {FilmId} wasn't found in the database.", id);
            return NotFound();
        }

        await _filmService.DeleteFilmByIdAsync(id, cancellationToken);

        return Ok();
    }
}
