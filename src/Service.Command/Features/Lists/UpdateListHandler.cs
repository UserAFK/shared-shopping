using Infrastructure.Data;
using MediatR;

namespace Service.Command.Features.ShoppingLists;

public class UpdateListHandler : IRequestHandler<UpdateListCommand, Guid>
{
    private readonly IShoppingDbContext _context;

    public UpdateListHandler(IShoppingDbContext context) => _context = context;

    public async Task<Guid> Handle(UpdateListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = _context.ShoppingLists
            .FirstOrDefault(l => l.Id == request.ShoppingList.Id)
            ?? throw new KeyNotFoundException($"Shopping list with ID {request.ShoppingList.Id} not found.");

        shoppingList.Name = request.ShoppingList.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return request.ShoppingList.Id;
    }
}
