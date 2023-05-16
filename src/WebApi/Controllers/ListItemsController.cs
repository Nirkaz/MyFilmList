using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListItemsController : ControllerBase
{
    private readonly ILogger<ListItemsController> _logger;
    private readonly IListItemService _listItemService;

    public ListItemsController(ILogger<ListItemsController> logger, IListItemService listItemService) {
        _logger = logger;
        _listItemService = listItemService;
    }

    [HttpGet("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<ListItem>> GetListItemAsync(int id, CancellationToken cancellationToken) {
        var item = await _listItemService.GetListItemAsync(id, cancellationToken);

        if (item is null) {
            _logger.LogWarning("List item with id: {ListItemId} wasn't found in the database.", id);
            return NotFound();
        }

        return item;
    }

    [HttpGet("All")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<IEnumerable<ListItem>>> GetAllListItemsAsync(CancellationToken cancellationToken) {
        var items = await _listItemService.GetAllListItemsAsync(cancellationToken);

        if (items is null || !items.Any()) {
            _logger.LogWarning("There wasn't any list items in the database.");
            return NotFound();
        }

        return items.ToList();
    }

    [HttpGet("Status/{status}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<IEnumerable<ListItem>>> GetListItemsByStatusAsync(ListItemStatus status, CancellationToken cancellationToken) {
        var items = await _listItemService.GetAllListItemsAsync(cancellationToken);

        if (items is null || !items.Any()) {
            _logger.LogWarning("There wasn't any list items in the database.");
            return NotFound();
        }

        return items.Where(x => x.ListItemStatus == status).ToList();
    }

    [HttpPost("Create/FromFilm/{filmId:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateListItemFromFilmAsync(int filmId, CancellationToken cancellationToken) {
        var res = await _listItemService.CreateListItemFromFilmAsync(filmId, cancellationToken);

        if (res is null) {
            _logger.LogWarning("Attempted to create list item from non-existing film.");
            return NotFound();
        }

        return Ok(res.Id);
    }

    [HttpPut("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutListItemAsync(int id, ListItem item, CancellationToken cancellationToken) {
        if (id != item.Id) {
            _logger.LogWarning("Id's don't match.");
            return BadRequest();
        }

        if (!await _listItemService.CheckIfListItemExists(id, cancellationToken)) {
            _logger.LogWarning("List item with id: {ListItemId} wasn't found in the database.", id);
            return NotFound();
        }

        await _listItemService.UpdateListItemAsync(item, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> PostListItemAsync(ListItem item, CancellationToken cancellationToken) {
        await _listItemService.AddListItemAsync(item, cancellationToken);

        return CreatedAtAction(nameof(PostListItemAsync), new { id = item.Id }, item);
    }

    [HttpDelete("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteListItemAsync(int id, CancellationToken cancellationToken) {
        if (!await _listItemService.CheckIfListItemExists(id, cancellationToken)) {
            _logger.LogWarning("List item with id: {ListItemId} wasn't found in the database.", id);
            return NotFound();
        }

        await _listItemService.DeleteListItemByIdAsync(id, cancellationToken);

        return Ok();
    }
}
