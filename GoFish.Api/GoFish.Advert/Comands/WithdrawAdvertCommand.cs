using System;
using GoFish.Shared.Command;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class WithdrawAdvertCommand : UserCommand, ICommand<Advert>
    {
        public readonly Guid Id;

        public WithdrawAdvertCommand(Guid id, int userId) : base(userId)
        {
            Id = id;
        }
    }
}