using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public interface IAdvertFactory
    {
        Advert BuildNew(AdvertDto fromDto);
        Advert Update(Advert advert, AdvertDto fromDto);
    }
}