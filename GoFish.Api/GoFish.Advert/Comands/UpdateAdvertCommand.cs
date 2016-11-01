using GoFish.Shared.Command;
using GoFish.Shared.Dto;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class UpdateAdvertCommand : UserCommand, ICommand<Advert>
    {
        public UpdateAdvertCommand(AdvertDto advert, int userId) : base(userId)
        {
            Advert = advert;
        }

        public AdvertDto Advert { get; private set; }
    }
}