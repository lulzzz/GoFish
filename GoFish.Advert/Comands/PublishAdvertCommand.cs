namespace GoFish.Advert
{
    public class PublishAdvertCommand : ICommand<Advert>
    {
        public PublishAdvertCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}