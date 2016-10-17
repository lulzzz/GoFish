namespace GoFish.Shared.Interface
{
    public interface IMessageBroker<T>
    {
        void SendMessagesFor(T objectWithEventsToSend);
    }
}