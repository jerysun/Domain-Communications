using Zack.EventBus;

namespace EBWebApi2.EventHandlers
{
  [EventName("OrderCreated")] // Listen to the event message "OrderCreated", if you want to
                              // listen to more than one, then just add another annotation here
  public class EventHandler2 : JsonIntegrationEventHandler<OrderData>
  {
    public override async Task HandleJson(string eventName, OrderData? orderData)
    {
      if (eventName != "OrderCreated") return;

      if (orderData != null)
      {
        Console.WriteLine($"In EventHandler2 of the microservice EBWebApi2, the orderData.Id and orderData.Name are: {orderData.Id}, {orderData.Name}");
        await Task.Delay(500);
      }
      else
      {
        throw new ArgumentNullException();
      }
    }
  }

  public record OrderData(long Id, string Name, DateTime Date);
}
