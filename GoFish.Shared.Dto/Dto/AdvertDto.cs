﻿namespace GoFish.Shared.Dto
{
    public class AdvertDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int AdvertiserId { get; set; }
        public int CatchTypeId { get; set; }
        public int Status { get; set; }
    }
}