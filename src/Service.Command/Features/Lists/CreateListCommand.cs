using MediatR;

namespace Service.Command.Features.ShoppingLists;

public record CreateListCommand(string Name) : IRequest<Guid>;
