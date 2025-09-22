namespace Service.Query.DTO
{
    public class ShoppingItemDetailedDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Quantity { get; set; }
    }
}