using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Item : EntityBase
    {
        [Column(TypeName = "varchar(200)")]
        public required string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public Guid ShoppingListId { get; set; }
        public ShoppingList? ShoppingList { get; set; } = null;
    }
}
