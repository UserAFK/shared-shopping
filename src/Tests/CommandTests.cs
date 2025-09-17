using Core.Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Command.Features.Items;
using Service.Command.Features.ShoppingLists;

namespace SharedShopping.Tests;

public class CommandTests
{
    private const string groceriesListId = "ae43a1b0-cc04-4bfd-86ca-db901475837a";
    private const string electronicsListId = "ae43a1b0-cc04-4bfd-86ca-db901475837b";

    private async Task AddShoppingLists(ShoppingDbContext context)
    {
        var shoppingLists = new List<ShoppingList>
            {
                new () { Id = Guid.Parse(groceriesListId), Name = "Groceries" },
                new () { Id = Guid.Parse(electronicsListId), Name = "Electronics" }
            };
        context.ShoppingLists.AddRange(shoppingLists);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task AddShoppingListsCommandAsync_ReturnsNewShoppingListId()
    {
        var options = new DbContextOptionsBuilder<ShoppingDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingLists(context);

            var shoppingListCommand = new CreateShoppingListCommand("Home");
            

            var service = new CreateShoppingListHandler(context);

            // Act
            var result = await service.Handle(shoppingListCommand, CancellationToken.None);

            // Assert
            Assert.True(result != Guid.Empty);
            Assert.Equal(3, context.ShoppingLists.Count());

            context.Database.EnsureDeleted();
        }
    }

    [Fact]
    public async Task AddItemCommandAsync_ListExists_ReturnsNewItemtId()
    {
        var options = new DbContextOptionsBuilder<ShoppingDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingLists(context);

            var shoppingListCommand = new CreateItemCommand("Lamp", 1, Guid.Parse(electronicsListId));

            var service = new CreateItemHandler(context);

            // Act
            var result = await service.Handle(shoppingListCommand, CancellationToken.None);

            // Assert
            Assert.True(result != Guid.Empty);
            Assert.Equal(1, context.Items.Count());

            context.Database.EnsureDeleted();
        }

    }

    [Fact]
    public async Task AddItemCommandAsync_ListDoenstExist_ReturnsNewItemtId()
    {
        var options = new DbContextOptionsBuilder<ShoppingDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingLists(context);

            var shoppingListCommand = new CreateItemCommand("Lamp", 1, Guid.NewGuid());

            var service = new CreateItemHandler(context);

            // Act
            var result = () => service.Handle(shoppingListCommand, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync< ArgumentException>( result);

            context.Database.EnsureDeleted();
        }
    }
}
