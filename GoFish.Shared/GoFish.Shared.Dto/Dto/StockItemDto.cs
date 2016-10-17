using System;

namespace GoFish.Shared.Dto
{
    public class StockItemDto
    {
        public Guid Id { get; set; }
        public int ProductTypeId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int OwnerId { get; set; }
        public Guid AdvertId { get; set; }
    }
}