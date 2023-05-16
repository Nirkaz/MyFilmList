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
    public async Task<IActionResult> GetListItemAsync([FromRoute] int id, CancellationToken cancellationToken) {
        var item = await _listItemService.GetListItemAsync(id, cancellationToken);

        if (item is null) {
            _logger.LogWarning("List item with id: {ListItemId} wasn't found in the database.", id);
            return NotFound();
        }

        return Ok(item);
    }

    [HttpGet("All")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetAllListItemsAsync(CancellationToken cancellationToken) {
        var items = await _listItemService.GetAllListItemsAsync(cancellationToken);

        if (items is null || !items.Any()) {
            _logger.LogWarning("There wasn't any list items in the database.");
            return NotFound();
        }

        return Ok(items);
    }

    [HttpGet("Status")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetListItemsByStatusAsync([FromQuery] ListItemStatus status, CancellationToken cancellationToken) {
        var items = await _listItemService.GetAllListItemsAsync(cancellationToken);

        if (items is null || !items.Any()) {
            _logger.LogWarning("There wasn't any list items in the database.");
            return NotFound();
        }

        return Ok(items.Where(x => x.ListItemStatus == status)); // ToDo: move filtration out of controller action
    }

    [HttpPost("Create/FromFilm/{filmId:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateListItemFromFilmAsync([FromRoute] int filmId, CancellationToken cancellationToken) {
        var result = await _listItemService.CreateListItemFromFilmAsync(filmId, cancellationToken);

        if (result is null) {
            _logger.LogWarning("Attempted to create list item from non-existing film.");
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutListItemAsync([FromRoute] int id, [FromBody] ListItem item, CancellationToken cancellationToken) {
        if (id != item.Id) {
            _logger.LogWarning("Id's don't match.");
            return BadRequest();
        }

        var existsCheck = await _listItemService.CheckIfListItemExists(id, cancellationToken);

        if (!existsCheck) {
            _logger.LogWarning("List item with id: {ListItemId} wasn't found in the database.", id);
            return NotFound();
        }

        await _listItemService.UpdateListItemAsync(item, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> PostListItemAsync([FromBody] ListItem item, CancellationToken cancellationToken) {
        await _listItemService.AddListItemAsync(item, cancellationToken);

        return CreatedAtAction(nameof(PostListItemAsync), new { id = item.Id }, item);
    }

    [HttpDelete("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteListItemAsync([FromRoute] int id, CancellationToken cancellationToken) {
        var existsCheck = await _listItemService.CheckIfListItemExists(id, cancellationToken);

        if (!existsCheck) {
            _logger.LogWarning("List item with id: {ListItemId} wasn't found in the database.", id);
            return NotFound();
        }

        await _listItemService.DeleteListItemByIdAsync(id, cancellationToken);

        return Ok();
    }
}
