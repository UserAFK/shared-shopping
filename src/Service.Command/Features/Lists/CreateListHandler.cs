using Core.Domain.Entities;
using Infrastructure.Data;
using MediatR;

namespace Service.Command.Features.ShoppingLists;

public class CreateListHandler : IRequestHandler<CreateListCommand, Guid>
{
    private readonly IShoppingDbContext _context;

    public CreateListHandler(IShoppingDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        var list = new ShoppingList { Id = Guid.NewGuid(), Name = request.Name };
        _context.ShoppingLists.Add(list);
        await _context.SaveChangesAsync(cancellationToken);
        return list.Id;
    }
}
