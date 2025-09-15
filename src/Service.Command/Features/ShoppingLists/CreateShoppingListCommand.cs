using MediatR;

namespace Service.Command.Features.ShoppingLists;

public record CreateShoppingListCommand(string Name) : IRequest<Guid>;
