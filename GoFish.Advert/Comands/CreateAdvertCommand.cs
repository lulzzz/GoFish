using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class CreateAdvertCommand : ICommand<Advert>
    {
        public readonly AdvertDto Advert;

        public CreateAdvertCommand(AdvertDto advert)
        {
            Advert = advert;
        }
    }
}