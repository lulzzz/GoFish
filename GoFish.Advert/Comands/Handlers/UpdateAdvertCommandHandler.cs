using System;

namespace GoFish.Advert
{
    public class UpdateAdvertCommandHandler : ICommandHandler<UpdateAdvertCommand, Advert>
    {
        private readonly AdvertRepository _repository;

        public UpdateAdvertCommandHandler(AdvertRepository repository)
        {
            _repository = repository;
        }

        public void Handle(UpdateAdvertCommand command)
        {
            if (command.Advert.Status != AdvertStatus.Created)
            {
                throw new InvalidOperationException("Can only update adverts in the 'Created' Status");
            }

            _repository.Save(command.Advert);
        }
    }
}