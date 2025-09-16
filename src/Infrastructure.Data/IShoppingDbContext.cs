using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public interface IShoppingDbContext
{
    DbSet<ShoppingList> ShoppingLists { get; set; }
    DbSet<Item> Items { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
