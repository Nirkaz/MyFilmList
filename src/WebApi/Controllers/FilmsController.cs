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
    public async Task<IActionResult> GetFilmAsync([FromRoute] int id, CancellationToken cancellationToken) {
        var film = await _filmService.GetFilmAsync(id, cancellationToken);
        return film is not null ? Ok(film) : NotFound();
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetAllFilmsAsync(CancellationToken cancellationToken) {
        var films = await _filmService.GetAllFilmsAsync(cancellationToken);
        return films?.Any() == true ? Ok(films) : NotFound();
    }

    [HttpPut("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutFilmAsync([FromRoute] int id, [FromBody] Film film, CancellationToken cancellationToken) {
        if (id != film.Id) {
            _logger.LogWarning("Id's don't match.");
            return BadRequest();
        }

        var result = await _filmService.UpdateFilmAsync(film, cancellationToken);

        return result ? NoContent() : NotFound();
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> PostFilmAsync([FromBody] Film film, CancellationToken cancellationToken) {
        var result = await _filmService.AddFilmAsync(film, cancellationToken);

        return result ? CreatedAtAction("GetFilm", new { id = film.Id }, film) : BadRequest();
    }

    [HttpDelete("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteFilmAsync([FromRoute] int id, CancellationToken cancellationToken) {
        var result = await _filmService.DeleteFilmByIdAsync(id, cancellationToken);
        
        return result ? Ok() : NotFound();
    }
}
