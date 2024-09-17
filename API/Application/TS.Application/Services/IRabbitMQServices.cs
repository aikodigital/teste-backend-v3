namespace TS.Application.Services
{
    public interface IRabbitMQServices
    {
        void Publisher(MessageQueue message);
    }
}