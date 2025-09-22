using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Query.Features.ShoppingList;

namespace Service.Query.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListController : ControllerBase
{
    private readonly IMediator _mediator;

    public ListController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Route("/lists")]
    public async Task<IActionResult> GetAllShoppingLists()
    {
        var query = new GetAllListsQuery(10);// Default to 10 if not specified
        var shoppingListDto = await _mediator.Send(query);
        return Ok(shoppingListDto);
    }

    [HttpGet]
    [Route("/lists/{id}")]
    public async Task<IActionResult> GetShoppingListById([FromRoute] Guid id)
    {
        var query = new GetListByIdQuery(id);
        var shoppingListDto = await _mediator.Send(query);
        return Ok(shoppingListDto);
    }
}
