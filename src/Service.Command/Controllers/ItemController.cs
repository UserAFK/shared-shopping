using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Command.Features.Items;

namespace Service.Command.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateItemCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }
}
