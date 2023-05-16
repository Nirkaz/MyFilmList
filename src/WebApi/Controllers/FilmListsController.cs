using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmListsController : ControllerBase
{
    private readonly ILogger<FilmListsController> _logger;
    private readonly IFilmListService _filmListService;

    public FilmListsController(ILogger<FilmListsController> logger, IFilmListService filmListService) {
        _logger = logger;
        _filmListService = filmListService;
    }

    [HttpGet("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetFilmListAsync([FromRoute] int id, CancellationToken cancellationToken) {
        var filmList = await _filmListService.GetFilmListAsync(id, cancellationToken);

        if (filmList is null) {
            _logger.LogWarning("Film list with id: {FilmListId} wasn't found in the database.", id);
            return NotFound();
        }

        return Ok(filmList);
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetAllFilmListsAsync(CancellationToken cancellationToken) {
        var filmLists = await _filmListService.GetAllFilmListsAsync(cancellationToken);

        if (filmLists is null || !filmLists.Any()) {
            _logger.LogWarning("There wasn't any films lists in the database.");
            return NotFound();
        }

        return Ok(filmLists);
    }

    [HttpPut("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutFilmListAsync([FromRoute] int id, [FromBody] FilmList filmList, CancellationToken cancellationToken) {
        if (id != filmList.Id) {
            _logger.LogWarning("Id's don't match.");
            return BadRequest();
        }

        var existsCheck = await _filmListService.CheckIfFilmListExists(id, cancellationToken);

        if (!existsCheck) {
            _logger.LogWarning("Film list with id: {FilmListId} wasn't found in the database.", id);
            return NotFound();
        }

        await _filmListService.UpdateFilmListAsync(filmList, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> PostFilmListAsync([FromBody] FilmList filmList, CancellationToken cancellationToken) {
        await _filmListService.AddFilmListAsync(filmList, cancellationToken);

        return CreatedAtAction(nameof(PostFilmListAsync), new { id = filmList.Id }, filmList);
    }

    [HttpPost("AddItem")]
    public async Task<IActionResult> AddItemToListAsync([FromQuery] int listId, [FromQuery] int itemId, CancellationToken cancellationToken)
        => await _filmListService.AddItemToListAsync(listId, itemId, cancellationToken) ? Ok() : NotFound();

    [HttpPost("CreateEmpty")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateEmptyListAsync(CancellationToken cancellationToken) {
        var id = await _filmListService.CreateEmptyListAsync(cancellationToken);

        return Ok(id);
    }

    [HttpDelete("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteFilmListAsync([FromRoute] int id, CancellationToken cancellationToken) {
        var existsCheck = await _filmListService.CheckIfFilmListExists(id, cancellationToken);

        if (!existsCheck) {
            _logger.LogWarning("Film list with id: {FilmListId} wasn't found in the database.", id);
            return NotFound();
        }

        await _filmListService.DeleteFilmListByIdAsync(id, cancellationToken);

        return Ok();
    }
}
