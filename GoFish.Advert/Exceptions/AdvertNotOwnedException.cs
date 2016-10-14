using System;

namespace GoFish.Advert
{
    public sealed class AdvertNotOwnedException : Exception
    {
        public AdvertNotOwnedException(string message) : base(message) { }
    }
}