using System;

namespace GoFish.Advert
{
    public class UpdateAdvertCommandHandler : AdvertCommandHandler<UpdateAdvertCommand>
    {
        private readonly IAdvertFactory _factory;

        public UpdateAdvertCommandHandler(AdvertRepository repository, IAdvertFactory factory) : base(repository)
        {
            _factory = factory;
        }

        public override void Handle(UpdateAdvertCommand command)
        {
            var advert = Repository.Get(command.Advert.Id);

            if (advert.Status != AdvertStatus.Created)
                throw new InvalidOperationException("Can only update adverts in the 'Created' Status");

            if(advert.Advertiser.Id != command.UserId)
                throw new AdvertNotOwnedException($"Advert not yours: {command.Advert.Id}");

            // Do it!
            var changedAdvert = _factory.Update(advert, command.Advert);

            Repository.Save(changedAdvert);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh here but this needs changing.
            RefreshReadModel(changedAdvert);
        }
    }
}