using RabbitMQ.Client;
using System.Text;

const string rabbitUrl = "YOUR_RABBITMQ_URL";
const string exchangeName = "exchange1";
const string eventName = "myEvent"; // The value of routingKey

var connFactory = new ConnectionFactory { Uri = new Uri(rabbitUrl) };
connFactory.DispatchConsumersAsync = true;

using var connection = connFactory.CreateConnection();
while (true)
{
  string msg = DateTime.UtcNow.TimeOfDay.ToString();
  using  (var channel = connection.CreateModel())
  {
    var properties = channel.CreateBasicProperties();
    properties.DeliveryMode = 2; //Non-persistent (1) or persistent (2)
    channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
    byte[] body = Encoding.UTF8.GetBytes(msg);
    channel.BasicPublish(exchange: exchangeName, routingKey: eventName, mandatory: true, basicProperties: properties, body: body);
  }
  Console.WriteLine($"Published the message: {msg}");
  Thread.Sleep(1000);
}
