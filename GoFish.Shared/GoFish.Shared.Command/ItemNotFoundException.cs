using System;

namespace GoFish.Shared.Command
{
    public sealed class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message) { }
    }
}