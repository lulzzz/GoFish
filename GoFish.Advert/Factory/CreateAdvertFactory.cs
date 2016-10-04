using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class CreateAdvertFactory : AdvertFactory, IAdvertFactory
    {
        public CreateAdvertFactory(AdvertDto data) : base(data) { }

        public override Advert Build()
        {
            ResultingAdvert = new Advert(
                Data.Id,
                CatchType.FromId(Data.CatchTypeId),
                Data.Quantity,
                Data.Price,
                Advertiser.FromId(Data.AdvertiserId));

            TransferCommonProperties();

            return ResultingAdvert;
        }
    }
}