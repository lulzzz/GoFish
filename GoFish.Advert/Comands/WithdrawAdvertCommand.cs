using System;

namespace GoFish.Advert
{
    public class WithdrawAdvertCommand : ICommand<Advert>
    {
        public readonly Guid Id;

        public WithdrawAdvertCommand(Guid id)
        {
            Id = id;
        }
    }
}