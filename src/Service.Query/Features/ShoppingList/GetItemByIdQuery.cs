using MediatR;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public record GetItemByIdQuery(Guid ItemId) : IRequest<ShoppingItemDetailedDto>;
