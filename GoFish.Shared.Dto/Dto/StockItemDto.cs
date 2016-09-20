namespace GoFish.Shared.Dto
{
    public class StockItemDto
    {
        public int ProductTypeId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int OwnerId { get; set; }
        public int AdvertId { get; set; }
    }
}