using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Query.Features.ShoppingList;

namespace Service.Query.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShoppingItemController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Route("/items")]
    public async Task<IActionResult> GetAllShoppingItems()
    {
        var query = new GetAllItemsQuery(10);// Default to 10 if not specified
        var itemDtos = await _mediator.Send(query);
        return Ok(itemDtos);
    }

    [HttpGet]
    [Route("/items/{id}")]
    public async Task<IActionResult> GetShoppingItemById([FromRoute] Guid id)
    {
        var query = new GetItemByIdQuery(id);
        var itemDto = await _mediator.Send(query);
        return Ok(itemDto);
    }
}
