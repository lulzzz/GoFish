namespace GoFish.Advert
{
    public class CreateAdvertCommandHandler : AdvertCommandHandler<CreateAdvertCommand>
    {
        private readonly IAdvertFactory _factory;

        public CreateAdvertCommandHandler(AdvertRepository repository, IAdvertFactory factory) : base(repository)
        {
            _factory = factory;
        }

        public override void Handle(CreateAdvertCommand command)
        {
            // construct an advert
            var advert = _factory.BuildNew(command.Advert);

            // Act
            advert.Create();

            // Save resulting events
            SaveEvents(advert);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh here but this needs changing.
            RefreshReadModel(advert);
        }
    }
}