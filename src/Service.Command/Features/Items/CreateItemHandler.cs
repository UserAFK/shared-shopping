using Core.Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Service.Command.Features.Items
{
    public class CreateItemHandler : IRequestHandler<CreateItemCommand, Guid>
    {
        private readonly IShoppingDbContext _context;

        public CreateItemHandler(IShoppingDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var hasList = await _context.ShoppingLists
                .AsNoTracking()
                .AnyAsync(l => l.Id == request.ShoppingListId, cancellationToken);
            if (!hasList)
            {
                throw new ArgumentException($"Shopping list ID {request.ShoppingListId} does not exist");
            }

            var item = new Item
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Quantity = request.Quantity,
                ShoppingListId = request.ShoppingListId
            };
            await _context.Items.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
