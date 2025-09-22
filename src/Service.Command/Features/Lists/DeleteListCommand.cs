using MediatR;

namespace Service.Command.Features.ShoppingLists;

public record DeleteListCommand(Guid ShoppingListId) : IRequest<Guid>;
