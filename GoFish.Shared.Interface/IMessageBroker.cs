namespace GoFish.Shared.Interface
{
    public interface IMessageBroker<T>
    {
        void Send(string message, T objectToSend);
    }
}