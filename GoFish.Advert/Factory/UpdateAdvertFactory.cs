using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class UpdateAdvertFactory : AdvertFactory, IAdvertFactory
    {
        private readonly Advert _oldState;

        public UpdateAdvertFactory(Advert oldState, AdvertDto newState) : base(newState)
        {
            _oldState = oldState;
        }

        public override Advert Build()
        {
            ResultingAdvert = new Advert(
                _oldState.Id,
                CatchType.FromId(Data.CatchTypeId),
                Data.Quantity,
                Data.Price,
                Advertiser.FromId(Data.AdvertiserId));

            ResultingAdvert.Status = (AdvertStatus)Data.Status;

            TransferCommonProperties();

            return ResultingAdvert;
        }
    }
}