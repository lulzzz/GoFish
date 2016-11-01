namespace GoFish.Shared.Interface
{
    public interface ICommandHandler<in TCommand, out TResult>
        where TCommand : ICommand<TResult>
        where TResult : ICommandable
    {
        void Handle(TCommand command);
    }
}