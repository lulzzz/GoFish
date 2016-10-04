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

            Save(command.Advert); // TODO: Save the "Created" event with the data payload
        }
    }
}