using MediatR;

namespace Service.Command.Features.ShoppingLists;

public record DeleteShoppingListCommand(Guid ShoppingListId) : IRequest<Guid>;
