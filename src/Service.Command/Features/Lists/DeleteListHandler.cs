using Infrastructure.Data;
using MediatR;

namespace Service.Command.Features.ShoppingLists;

public class DeleteListHandler : IRequestHandler<DeleteListCommand, Guid>
{
    private readonly IShoppingDbContext _context;

    public DeleteListHandler(IShoppingDbContext context) => _context = context;

    public async Task<Guid> Handle(DeleteListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = _context.ShoppingLists
            .FirstOrDefault(l => l.Id == request.ShoppingListId)
            ?? throw new KeyNotFoundException($"Shopping list with ID {request.ShoppingListId} not found.");
        _context.ShoppingLists.Remove(shoppingList);
        await _context.SaveChangesAsync(cancellationToken);
        return request.ShoppingListId;
    }
}
