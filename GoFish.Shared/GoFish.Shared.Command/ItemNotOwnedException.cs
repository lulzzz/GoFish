using System;

namespace GoFish.Shared.Command
{
    public sealed class ItemNotOwnedException : Exception
    {
        public ItemNotOwnedException(string message) : base(message) { }
    }
}