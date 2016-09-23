using System;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class PostAdvertCommandHandler : ICommandHandler<PostAdvertCommand, Advert>
    {
        private readonly AdvertRepository _repository;
        private readonly IMessageBroker<Advert> _messageBroker;

        public PostAdvertCommandHandler(AdvertRepository repository, IMessageBroker<Advert> messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public Advert Handle(PostAdvertCommand command)
        {
            var advert = _repository.Get(command.Id);

            if (advert == null)
            {
                throw new AdvertNotFoundException($"Advert {command.Id} not found.");
            }

            if (advert.Status != AdvertStatus.Created)
            {
                throw new InvalidOperationException("Can only post non-posted adverts.");
            }

            advert.Post();

            _repository.Save(advert);
            _messageBroker.Send(advert);

            return advert;
        }
    }

    public sealed class AdvertNotFoundException : Exception
    {
        public AdvertNotFoundException(string message) : base(message) { }
    }
}