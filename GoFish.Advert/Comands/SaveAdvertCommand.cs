namespace GoFish.Advert
{
    public class SaveAdvertCommand : ICommand<Advert>
    {
        public SaveAdvertCommand(Advert advert)
        {
            Advert = advert;
        }

        public Advert Advert { get; private set; }
    }
}