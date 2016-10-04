namespace GoFish.Advert
{
    public class UpdateAdvertCommand : ICommand<Advert>
    {
        public UpdateAdvertCommand(Advert advert)
        {
            Advert = advert;
        }

        public Advert Advert { get; private set; }
    }
}