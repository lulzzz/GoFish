using System;
using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class UpdateAdvertFactory : AdvertFactory
    {
        private readonly Advert _oldState;

        public UpdateAdvertFactory(Advert oldState, AdvertDto newState) : base(newState)
        {
            _oldState = oldState;
        }

        public override Advert Build()
        {
            if (_oldState.Status != AdvertStatus.Created)
            {
                throw new InvalidOperationException("Can only update Adverts in the 'Created' Status");
            }

            ResultingAdvert = Advert.Attach(_oldState.Id,
                CatchType.FromId(Data.CatchTypeId),
                Data.Quantity,
                Data.Price,
                Advertiser.FromId(Data.AdvertiserId));

            TransferCommonProperties();

            return ResultingAdvert;
        }
    }
}