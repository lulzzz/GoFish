using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class CreateAdvertFactory : AdvertFactory
    {
        public CreateAdvertFactory(AdvertDto data) : base(data) { }

        public override Advert Build()
        {
            CreatedAdvert = Advert.Add(
                CatchType.FromId(Data.CatchTypeId),
                Data.Quantity,
                Data.Price,
                Advertiser.FromId(Data.AdvertiserId));

            TransferCommonProperties();

            return CreatedAdvert;
        }
    }
}