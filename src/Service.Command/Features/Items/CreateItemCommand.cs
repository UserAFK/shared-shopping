using MediatR;

namespace Service.Command.Features.Items
{
    public record CreateItemCommand(string Name, int Quantity, Guid ShoppingListId) : IRequest<Guid>;
}
