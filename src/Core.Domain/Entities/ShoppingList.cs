namespace Core.Domain.Entities
{
    public class ShoppingList: EntityBase
    {
        public string Name { get; set; } = null!;
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
