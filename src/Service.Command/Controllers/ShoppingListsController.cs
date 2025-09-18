using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Command.Features.ShoppingLists;

namespace Service.Command.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingListsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShoppingListsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShoppingListCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateShoppingListCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteShoppingListCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }
}
