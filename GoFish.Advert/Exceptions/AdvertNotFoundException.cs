using System;

namespace GoFish.Advert
{
    public sealed class AdvertNotFoundException : Exception
    {
        public AdvertNotFoundException(string message) : base(message) { }
    }
}