using System;
using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class CreateAdvertCommand : UserCommand
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