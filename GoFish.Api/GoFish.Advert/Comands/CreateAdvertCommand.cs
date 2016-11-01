using System;
using GoFish.Shared.Dto;
using GoFish.Shared.Command;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class CreateAdvertCommand : UserCommand, ICommand<Advert>
    {
        public Guid Id
        {
            get
            {
                return Advert.Id;
            }
        }

        public readonly AdvertDto Advert;

        public CreateAdvertCommand(AdvertDto advert, int userId) : base(userId)
        {
            Advert = advert;
        }
    }
}