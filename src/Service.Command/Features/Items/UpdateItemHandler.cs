using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Service.Command.Features.Items
{
    public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, Guid>
    {
        private readonly IShoppingDbContext _context;

        public UpdateItemHandler(IShoppingDbContext context) => _context = context;

        public async Task<Guid> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var hasList = await _context.ShoppingLists
                .AsNoTracking()
                .AnyAsync(l => l.Id == request.Item.ShoppingListId, cancellationToken);
            if (!hasList)
            {
                throw new ArgumentException($"Shopping list ID {request.Item.ShoppingListId} does not exist");
            }
            var item = _context.Items
                .FirstOrDefault(i => i.Id == request.Item.Id)
                ?? throw new KeyNotFoundException($"Item with ID {request.Item.Id} not found.");

            item.Name = request.Item.Name;
            item.Quantity = request.Item.Quantity;
            item.ShoppingListId = request.Item.ShoppingListId;

            await _context.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
