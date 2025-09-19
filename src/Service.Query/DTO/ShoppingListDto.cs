namespace Service.Query.DTO;

public class ShoppingListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ShoppingItemDto>? Items { get; set; } = new();
}
