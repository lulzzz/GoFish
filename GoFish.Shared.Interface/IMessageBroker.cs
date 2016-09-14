namespace GoFish.Shared.Interface
{
    public interface IMessageBroker<T>
    {
        void Send(T objectToSend);
    }
}
