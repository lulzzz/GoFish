namespace GoFish.Advert
{
    public class PostAdvertCommand : ICommand<Advert>
    {
        public PostAdvertCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}