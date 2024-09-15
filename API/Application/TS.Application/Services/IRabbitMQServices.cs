namespace TS.Application.Services
{
    public interface IRabbitMQServices
    {
        void Publisher(string message);
        string Consumer();
    }
}