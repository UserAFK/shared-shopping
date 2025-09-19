using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Query.DTO;
using Service.Query.Features.ShoppingList;

namespace Service.Query.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingListController: ControllerBase
{
    private readonly IMediator _mediator;

    public ShoppingListController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Route("/lists/{id}")]
    public async Task<IActionResult> GetShoppingListById([FromRoute] Guid id)
    {
        var query = new GetShoppingListByIdQuery(id);
        var shoppingListDto = await _mediator.Send(query);
        return Ok(shoppingListDto);
    }

    [HttpGet]
    [Route("/lists")]
    public async Task<IActionResult> GetAllShoppingLists()
    {
        var query = new GetAllShoppingListsQuery(10);// Default to 10 if not specified
        var shoppingListDto = await _mediator.Send(query);
        return Ok(shoppingListDto);
    }
}
