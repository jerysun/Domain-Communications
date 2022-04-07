using Zack.EventBus;

namespace EBWebApi1.EventHandlers
{
  [EventName("OrderCreated")] // Listen to the event message "OrderCreated", if you want to
                              // listen to more than one, then just add another annotation here
  public class EventHandler1 : IIntegrationEventHandler
  {
    public async Task Handle(string eventName, string eventData)
    {
      if (eventName == "OrderCreated")
      {
        Console.WriteLine($"In EventHanlder1 of microservice EBWebApi1 we received the order, eventData: {eventData}");
      }

      await Task.Delay(500);
    }
  }
}