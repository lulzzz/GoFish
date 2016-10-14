namespace GoFish.Advert
{
    public class UserCommand : ICommand<Advert>
    {
        public readonly int UserId;

        public UserCommand (int userId)
        {
            UserId = userId;
        }
    }
}