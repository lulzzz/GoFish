using System;

namespace GoFish.Advert
{
    public class WithdrawAdvertCommand : UserCommand
    {
        public readonly Guid Id;

        public WithdrawAdvertCommand(Guid id, int userId) : base(userId)
        {
            Id = id;
        }
    }
}