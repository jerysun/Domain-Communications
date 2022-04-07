namespace Zack.EventBus
{
    public class IntegrationEventRabbitMQOptions
    {
        public string HostName { get; set; } = string.Empty;
        public string ExchangeName { get; set; } = string.Empty;
        public string RabbitUrl { get; set; } = string.Empty;
    }
}
