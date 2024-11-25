using Aplication.Services.Queue;

namespace TesteBackendV3
{
    public class ServiceBusBackgroundWorker : BackgroundService
    {
        private readonly ServiceBusConsumer _consumer;

        public ServiceBusBackgroundWorker(ServiceBusConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartProcessingAsync();
        }
    }

}
