using System;

namespace GoFish.Shared.Dto
{
    public class AdvertDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int AdvertiserId { get; set; }
        public int CatchTypeId { get; set; }
        public string Pitch { get; set; }
        public int FishingMethodId { get; set; }
        public AdvertStatus Status { get; set; } = AdvertStatus.Creating;
    }

    public enum AdvertStatus
    {
        Creating,
        Created
    }
}