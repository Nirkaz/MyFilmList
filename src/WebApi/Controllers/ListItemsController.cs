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
        return item is not null ? Ok(item) : NotFound();
    }

    [HttpGet("All")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetAllListItemsAsync(CancellationToken cancellationToken) {
        var items = await _listItemService.GetAllListItemsAsync(cancellationToken);
        return items?.Any() == true ? Ok(items) : NotFound();
    }

    [HttpGet("Status")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetListItemsByStatusAsync([FromQuery] ListItemStatus status, CancellationToken cancellationToken) {
        var items = await _listItemService.GetListItemsByStatus(status, cancellationToken);
        return items?.Any() == true ? Ok(items) : NotFound();
    }

    [HttpPut("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutListItemAsync([FromRoute] int id, [FromBody] ListItem item, CancellationToken cancellationToken) {
        if (id != item.Id) {
            _logger.LogWarning("Id's don't match.");
            return BadRequest();
        }

        var result = await _listItemService.UpdateListItemAsync(item, cancellationToken);
        return result ? NoContent() : NotFound();
    }

    [HttpPost("Create/FromFilm/{filmId:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> CreateListItemFromFilmAsync([FromRoute] int filmId, CancellationToken cancellationToken) {
        var result = await _listItemService.CreateListItemFromFilmAsync(filmId, cancellationToken);
        return result is not null ? CreatedAtAction("GetListItem", new { id = result.Id }, result) : BadRequest();
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> PostListItemAsync([FromBody] ListItem item, CancellationToken cancellationToken) {
        var result = await _listItemService.AddListItemAsync(item, cancellationToken);
        return result ? CreatedAtAction("GetListItem", new { id = item.Id }, item) : BadRequest();
    }

    [HttpDelete("{id:int:min(1)}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteListItemAsync([FromRoute] int id, CancellationToken cancellationToken) {
        var result = await _listItemService.DeleteListItemByIdAsync(id, cancellationToken);
        return result ? Ok() : NotFound();
    }
}
