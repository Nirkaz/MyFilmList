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
        return filmList is not null ? Ok(filmList) : NotFound();
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetAllFilmListsAsync(CancellationToken cancellationToken) {
        var filmLists = await _filmListService.GetAllFilmListsAsync(cancellationToken);
        return filmLists?.Any() == true ? Ok(filmLists) : NotFound();
    }

    [HttpPut("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutFilmListAsync([FromRoute] int id, [FromBody] FilmList filmList, CancellationToken cancellationToken) {
        if (id != filmList.Id) {
            _logger.LogWarning("Id's don't match.");
            return BadRequest();
        }

        var result = await _filmListService.UpdateFilmListAsync(filmList, cancellationToken);
        return result ? NoContent() : NotFound();
    }

    [HttpPut("AddItem")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> AddItemToListAsync([FromQuery] int listId, [FromQuery] int itemId, CancellationToken cancellationToken) {
        if (listId <= 0 ||  itemId <= 0) {
            _logger.LogWarning("Id's should be more than 0");
            return BadRequest();
        }

        var result = await _filmListService.AddItemToListAsync(listId, itemId, cancellationToken);
        return result ? NoContent() : NotFound();
    }

    [HttpPut("RemoveItem")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> RemoveItemFromListAsync([FromQuery] int listId, [FromQuery] int itemId, CancellationToken cancellationToken) {
        if (listId <= 0 || itemId <= 0) {
            _logger.LogWarning("Id's should be more than 0");
            return BadRequest();
        }

        var result = await _filmListService.RemoveItemFromListAsync(listId, itemId, cancellationToken);
        return result ? NoContent() : NotFound();
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> PostFilmListAsync([FromBody] FilmList filmList, CancellationToken cancellationToken) {
        var result = await _filmListService.AddFilmListAsync(filmList, cancellationToken);
        return result ? CreatedAtAction("GetFilmList", new { id = filmList.Id }, filmList) : BadRequest();
    }

    [HttpPost("CreateEmpty")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> CreateEmptyListAsync(CancellationToken cancellationToken) {
        var result = await _filmListService.CreateEmptyListAsync(cancellationToken);
        return result is not null ? CreatedAtAction("GetFilmList", new { id = result.Id }, result) : BadRequest();
    }

    [HttpDelete("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteFilmListAsync([FromRoute] int id, CancellationToken cancellationToken) {
        var result = await _filmListService.DeleteFilmListByIdAsync(id, cancellationToken);
        return result ? Ok() : NotFound();
    }
}
