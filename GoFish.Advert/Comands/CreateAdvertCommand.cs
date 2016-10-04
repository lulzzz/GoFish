namespace GoFish.Advert
{
    public class CreateAdvertCommand : ICommand<Advert>
    {
        public CreateAdvertCommand(Advert advert)
        {
            Advert = advert;
        }

        public Advert Advert { get; private set; }
    }
}