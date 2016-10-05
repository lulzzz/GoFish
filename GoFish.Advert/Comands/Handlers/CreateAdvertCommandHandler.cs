using System;

namespace GoFish.Advert
{
    public class CreateAdvertCommandHandler : AdvertCommandHandler<CreateAdvertCommand>
    {
        public CreateAdvertCommandHandler(AdvertRepository repository) : base(repository) { }

        public override void Handle(CreateAdvertCommand command)
        {
            // business rules
            if (command.Advert.Status != AdvertStatus.Creating)
            {
                throw new InvalidOperationException("Can only create adverts currently in the 'Creating' status");
            }

            // Call the create method which can validate the invariants & generate events.
            command.Advert.Create();

            SaveEvents(command.Advert);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh all (this needs changing!)
            RefreshReadModel(command.Advert);
        }
    }
}