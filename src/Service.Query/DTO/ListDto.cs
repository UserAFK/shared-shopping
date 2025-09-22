namespace Service.Query.DTO;

public class ListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ItemDto>? Items { get; set; } = new();
}
