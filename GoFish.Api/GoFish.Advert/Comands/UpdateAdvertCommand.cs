using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class UpdateAdvertCommand : UserCommand
    {

        public UpdateAdvertCommand(AdvertDto advert, int userId) : base(userId)
        {
            Advert = advert;
        }

        public AdvertDto Advert { get; private set; }
    }
}