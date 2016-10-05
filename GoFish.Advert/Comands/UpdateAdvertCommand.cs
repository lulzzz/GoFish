using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class UpdateAdvertCommand : ICommand<Advert>
    {
        public UpdateAdvertCommand(AdvertDto advert)
        {
            Advert = advert;
        }

        public AdvertDto Advert { get; private set; }
    }
}