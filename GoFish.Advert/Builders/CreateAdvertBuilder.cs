using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class CreateAdvertBuilder : AdvertBuilder
    {
        public CreateAdvertBuilder(AdvertDto data) : base(data) { }

        public override Advert Build()
        {
            return Advert.Add(
                CatchType.FromId(Data.CatchTypeId),
                Data.Quantity,
                Data.Price,
                Advertiser.FromId(Data.AdvertiserId));
        }
    }
}