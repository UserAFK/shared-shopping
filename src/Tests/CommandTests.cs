using Core.Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Command.Features.Items;
using Service.Command.Features.ShoppingLists;

namespace SharedShopping.Tests;

public class CommandTests
{
    private const string groceriesListId = "ae43a1b0-cc04-4bfd-86ca-db901475837a";
    private const string groceriesListItemId = "ae43a1b0-cc04-4bfd-86ca-db901475837a";
    private const string electronicsListId = "ae43a1b0-cc04-4bfd-86ca-db901475837b";
    private const string electronicsListItemId = "ae43a1b0-cc04-4bfd-86ca-db901475838b";

    private static async Task AddShoppingListsAndItems(ShoppingDbContext context)
    {
        var shoppingLists = new List<ShoppingList>
            {
                new () { Id = Guid.Parse(groceriesListId), Name = "Groceries" },
                new () { Id = Guid.Parse(electronicsListId), Name = "Electronics" }
            };
        context.ShoppingLists.AddRange(shoppingLists);
        await context.SaveChangesAsync();

        var items = new List<Item>
            {
                new ()
                {
                    Id = Guid.Parse(groceriesListItemId),
                    Name = "Orange",
                    Quantity = 1,
                    ShoppingListId = Guid.Parse(groceriesListId)
                },
                new ()
                {
                    Id = Guid.Parse(electronicsListItemId),
                    Name = "Smartphone",
                    Quantity = 1,
                    ShoppingListId = Guid.Parse(electronicsListId)
                }
            };
        context.Items.AddRange(items);
        await context.SaveChangesAsync();
    }

    private static DbContextOptions<ShoppingDbContext> CreateDbContextOptions()
        => new DbContextOptionsBuilder<ShoppingDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDb")
                    .Options;

    #region Shopping_list
    [Fact]
    public async Task AddShoppingListsCommandAsync_ReturnsNewShoppingListId()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            var command = new CreateListCommand("Home");


            var service = new CreateListHandler(context);

            // Act
            var result = await service.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result != Guid.Empty);
            Assert.Equal(1, context.ShoppingLists.Count());

            context.Database.EnsureDeleted();
        }
    }

    [Fact]
    public async Task UpdateShoppingListsCommandAsync_ReturnsShoppingListId()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);
            var electronicsList = new ShoppingList
            {
                Id = Guid.Parse(electronicsListId),
                Name = "Electronics and Gadgets"
            };
            var command = new UpdateListCommand(electronicsList);

            var service = new UpdateListHandler(context);

            // Act
            var result = await service.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result == Guid.Parse(electronicsListId));
            Assert.Equal(2, context.ShoppingLists.Count());

            context.Database.EnsureDeleted();
        }
    }

    [Fact]
    public async Task UpdateShoppingListsCommandAsync_ListDoesntExist_ThrowsError()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);
            var electronicsList = new ShoppingList
            {
                Id = Guid.NewGuid(),
                Name = "Electronics and Gadgets"
            };
            var command = new UpdateListCommand(electronicsList);

            var service = new UpdateListHandler(context);

            // Act
            var result = () => service.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(result);

            context.Database.EnsureDeleted();
        }
    }

    [Fact]
    public async Task DeleteShoppingListsCommandAsync_ReturnsShoppingListId()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);
            var command = new DeleteListCommand(Guid.Parse(electronicsListId));

            var service = new DeleteListHandler(context);

            // Act
            var result = await service.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result == Guid.Parse(electronicsListId));
            Assert.Equal(1, context.ShoppingLists.Count());

            context.Database.EnsureDeleted();
        }
    }

    [Fact]
    public async Task DeleteShoppingListsCommandAsync_ListDoesntExist_ThrowsError()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);
            var command = new DeleteListCommand(Guid.NewGuid());

            var service = new DeleteListHandler(context);

            // Act
            var result = () => service.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(result);

            context.Database.EnsureDeleted();
        }
    }
    #endregion

    #region Item
    [Fact]
    public async Task AddItemCommandAsync_ListExists_ReturnsNewItemtId()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);

            var command = new CreateItemCommand("Lamp", 1, Guid.Parse(electronicsListId));

            var service = new CreateItemHandler(context);

            // Act
            var result = await service.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result != Guid.Empty);
            Assert.Equal(3, context.Items.Count());

            context.Database.EnsureDeleted();
        }

    }

    [Fact]
    public async Task AddItemCommandAsync_ListDoenstExist_ThrowsError()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);

            var command = new CreateItemCommand("Lamp", 1, Guid.NewGuid());

            var service = new CreateItemHandler(context);

            // Act
            var result = () => service.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(result);

            context.Database.EnsureDeleted();
        }
    }

    [Fact]
    public async Task UpdateItemCommandAsync_ListExists_ReturnsItemtId()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);
            var item = new Item
            {
                Id = Guid.Parse(electronicsListItemId),
                Name = "Smartphone Pro",
                Quantity = 2,
                ShoppingListId = Guid.Parse(electronicsListId)
            };
            var command = new UpdateItemCommand(item);

            var service = new UpdateItemHandler(context);

            // Act
            var result = await service.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result == Guid.Parse(electronicsListItemId));
            Assert.Equal(2, context.Items.Count());

            context.Database.EnsureDeleted();
        }

    }

    [Fact]
    public async Task UpdateItemCommandAsync_ListDoenstExist_ThrowsError()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);
            var item = new Item
            {
                Id = Guid.Parse(electronicsListItemId),
                Name = "Smartphone Pro",
                Quantity = 2,
                ShoppingListId = Guid.NewGuid()
            };
            var command = new UpdateItemCommand(item);

            var service = new UpdateItemHandler(context);

            // Act
            var result = () => service.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(result);

            context.Database.EnsureDeleted();
        }
    }

    [Fact]
    public async Task UpdateItemCommandAsync_ItemDoenstExist_ThrowsError()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);
            var item = new Item
            {
                Id = Guid.NewGuid(),
                Name = "Smartphone Pro",
                Quantity = 2,
                ShoppingListId = Guid.Parse(electronicsListId)
            };
            var command = new UpdateItemCommand(item);

            var service = new UpdateItemHandler(context);

            // Act
            var result = () => service.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(result);

            context.Database.EnsureDeleted();
        }
    }

    [Fact]
    public async Task DeleteItemCommandAsync_ItemExists_ReturnsItemtId()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);
            var itemId = Guid.Parse(electronicsListItemId);
            var command = new DeleteItemCommand(itemId);

            var service = new DeleteItemHandler(context);

            // Act
            var result = await service.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result == Guid.Parse(electronicsListItemId));
            Assert.Equal(1, context.Items.Count());

            context.Database.EnsureDeleted();
        }

    }

    [Fact]
    public async Task DeleteItemCommandAsync_ItemDoenstExist_ThrowsError()
    {
        var options = CreateDbContextOptions();

        using (var context = new ShoppingDbContext(options))
        {
            // Arrange
            await AddShoppingListsAndItems(context);
            var itemId = Guid.NewGuid();
            var command = new DeleteItemCommand(itemId);

            var service = new DeleteItemHandler(context);

            // Act
            var result = () => service.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(result);

            context.Database.EnsureDeleted();
        }
    }

    #endregion
}
