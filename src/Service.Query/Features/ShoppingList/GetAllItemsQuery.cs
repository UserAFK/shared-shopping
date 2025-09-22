using MediatR;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public record GetAllItemsQuery(int? Count) : IRequest<ICollection<ItemDto>>;
