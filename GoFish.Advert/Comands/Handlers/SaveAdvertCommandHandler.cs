namespace GoFish.Advert
{
    public class SaveAdvertCommandHandler : ICommandHandler<SaveAdvertCommand, Advert>
    {
        private readonly AdvertRepository _repository;

        public SaveAdvertCommandHandler(AdvertRepository repository)
        {
            _repository = repository;
        }

        public Advert Handle(SaveAdvertCommand command)
        {
            return _repository.Save(command.Advert);
        }
    }
}