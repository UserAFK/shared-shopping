using Core.Domain.Entities;
using MediatR;

namespace Service.Command.Features.ShoppingLists;

public record UpdateListCommand(ShoppingList ShoppingList) : IRequest<Guid>;
