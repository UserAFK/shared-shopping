using Infrastructure.Data;
using MediatR;

namespace Service.Command.Features.Items
{
    public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, Guid>
    {
        private readonly IShoppingDbContext _context;

        public DeleteItemHandler(IShoppingDbContext context) => _context = context;

        public async Task<Guid> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = _context.Items
                .FirstOrDefault(i => i.Id == request.ItemId)
                ?? throw new KeyNotFoundException($"Item with ID {request.ItemId} not found.");
            _context.Items.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}
