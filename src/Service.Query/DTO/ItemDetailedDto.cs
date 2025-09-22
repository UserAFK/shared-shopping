namespace Service.Query.DTO
{
    public class ItemDetailedDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Quantity { get; set; }
    }
}