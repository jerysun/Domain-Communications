using Microsoft.AspNetCore.Mvc;
using Zack.EventBus;

namespace EBWebApi1.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IEventBus _eventBus;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IEventBus eventBus)
    {
      _logger = logger;
      _eventBus = eventBus;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
      // _eventBus.Publish("OrderCreated", 888); // this 888 will be "JSON serialized" by the receiver side
      _eventBus.Publish("OrderCreated", new OrderData(123, "Socks", DateTime.UtcNow));

      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }
  }

  record OrderData(long Id, string Name, DateTime Date);
}