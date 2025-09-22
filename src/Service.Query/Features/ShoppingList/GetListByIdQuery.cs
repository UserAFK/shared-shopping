using MediatR;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public record GetListByIdQuery(Guid ListId) : IRequest<ListDto>;
