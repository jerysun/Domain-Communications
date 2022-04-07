using Zack.EventBus;

namespace EBWebApi2.EventHandlers
{
  [EventName("OrderCreated")] // Listen to the event message "OrderCreated", if you want to
                              // listen to more than one, then just add another annotation here
  public class EventHandler3 : DynamicIntegrationEventHandler
  {
    public override async Task HandleDynamic(string eventName, dynamic eventData)
    {
      if (eventName != "OrderCreated") return;

      Console.WriteLine($"In EventHandler3 of the microservice EBWebApi2, the orderData.Id and orderData.Name are: {eventData.Id}, {eventData.Name}");
      await Task.Delay(500);
    }
  }
}
