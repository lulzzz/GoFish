namespace GoFish.Shared.Interface
{
    public interface ICommandMediator
    {
        void Send<TResult>(ICommand<TResult> query);
    }
}