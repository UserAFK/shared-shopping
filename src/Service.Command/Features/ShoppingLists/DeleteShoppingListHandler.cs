using Infrastructure.Data;
using MediatR;

namespace Service.Command.Features.ShoppingLists;

public class DeleteShoppingListHandler : IRequestHandler<DeleteShoppingListCommand, Guid>
{
    private readonly IShoppingDbContext _context;

    public DeleteShoppingListHandler(IShoppingDbContext context) => _context = context;

    public async Task<Guid> Handle(DeleteShoppingListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = _context.ShoppingLists
            .FirstOrDefault(l => l.Id == request.ShoppingListId)
            ?? throw new KeyNotFoundException($"Shopping list with ID {request.ShoppingListId} not found.");
        _context.ShoppingLists.Remove(shoppingList);
        await _context.SaveChangesAsync(cancellationToken);
        return request.ShoppingListId;
    }
}
