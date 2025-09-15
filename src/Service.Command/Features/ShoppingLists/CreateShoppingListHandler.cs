using Core.Domain.Entities;
using Infrastructure.Data;
using MediatR;

namespace Service.Command.Features.ShoppingLists;

public class CreateShoppingListHandler : IRequestHandler<CreateShoppingListCommand, Guid>
{
    private readonly ShoppingDbContext _context;

    public CreateShoppingListHandler(ShoppingDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
    {
        var list = new ShoppingList { Id = Guid.NewGuid(), Name = request.Name };
        _context.ShoppingLists.Add(list);
        await _context.SaveChangesAsync(cancellationToken);
        return list.Id;
    }
}
