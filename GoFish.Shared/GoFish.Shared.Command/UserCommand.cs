namespace GoFish.Shared.Command
{
    public abstract class UserCommand
    {
        public readonly int UserId;

        public UserCommand(int userId)
        {
            UserId = userId;
        }
    }
}