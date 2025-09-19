using MediatR;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public record GetAllShoppingListsQuery(int? Count) : IRequest<ICollection<ShoppingListDto>>;
