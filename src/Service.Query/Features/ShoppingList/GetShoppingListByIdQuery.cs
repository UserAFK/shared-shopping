using MediatR;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public record GetShoppingListByIdQuery(Guid Id) : IRequest<ShoppingListDto>;
