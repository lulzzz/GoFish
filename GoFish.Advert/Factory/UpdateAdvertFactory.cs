using System;
using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class UpdateAdvertFactory : AdvertFactory
    {
        public UpdateAdvertFactory(AdvertDto data) : base(data) { }

        public override Advert Build()
        {
            if ((AdvertStatus)Data.Status != AdvertStatus.Created)
            {
                throw new InvalidOperationException("Can only update Adverts in the 'Created' Status");
            }

            CreatedAdvert = Advert.Attach(
                Data.Id,
                CatchType.FromId(Data.CatchTypeId),
                Data.Quantity,
                Data.Price,
                Advertiser.FromId(Data.AdvertiserId));

            TransferCommonProperties();

            return CreatedAdvert;
        }
    }
}