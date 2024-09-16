namespace TheatricalPlayersRefactoringKata.Interface
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
