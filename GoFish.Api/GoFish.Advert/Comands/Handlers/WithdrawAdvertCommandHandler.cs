namespace GoFish.Advert
{
    public class WithdrawAdvertCommandHandler : AdvertCommandHandler<WithdrawAdvertCommand>
    {
        public WithdrawAdvertCommandHandler(AdvertRepository repository)
            : base(repository) { }

        public override void Handle(WithdrawAdvertCommand command)
        {
            var advert = Repository.Get(command.Id);

            if (advert == null)
                throw new AdvertNotFoundException($"Advert not found: {command.Id}");

            if(advert.Advertiser.Id != command.UserId)
                throw new AdvertNotOwnedException($"Advert not yours: {command.Id}");

            // Do it!
            advert.Withdraw();

            SaveEvents(advert);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh here but this needs changing
            RefreshReadModel(advert);
        }
    }
}