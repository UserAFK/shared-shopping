using Core.Domain.Entities;
using MediatR;

namespace Service.Command.Features.ShoppingLists;

public record UpdateShoppingListCommand(ShoppingList ShoppingList) : IRequest<Guid>;
