using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

const string rabbitUrl = "YOUR_RABBITMQ_URL";
const string exchangeName = "exchange1";
const string eventName = "myEvent"; // The value of routingKey
const string queueName = "queue1";

var connFactory = new ConnectionFactory { Uri = new Uri(rabbitUrl) };
connFactory.DispatchConsumersAsync = true;

using var connection = connFactory.CreateConnection();
using (var channel = connection.CreateModel())
{
  channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
  channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
  channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: eventName);

  // Here is the pull mode: the consumer will pull the event message from the queue, consumer is the puller
  var consumer = new AsyncEventingBasicConsumer(channel);
  consumer.Received += Consumer_Received;
  channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
  Console.WriteLine("Press Enter to exit");
  Console.ReadLine();

  async Task Consumer_Received(object sender, BasicDeliverEventArgs args)
  {
    try
    {
      var bytes = args.Body.ToArray();
      string msg = Encoding.UTF8.GetString(bytes);
      Console.WriteLine(DateTime.UtcNow + " Received the message: " + msg);
      //DeliveryTag is just the numbering of the message
      channel.BasicAck(args.DeliveryTag, multiple: false);
      await Task.Delay(800);
    }
    catch(Exception ex)
    {
      channel.BasicReject(args.DeliveryTag, true);
      Console.WriteLine("error occurred during dealing with the received message: " + ex);
    }
  }
}