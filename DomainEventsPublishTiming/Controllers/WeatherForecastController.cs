using DomainEventsPublishTiming.Data;
using Microsoft.AspNetCore.Mvc;

namespace DomainEventsPublishTiming.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly DomainEventsDbContext _ctx;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DomainEventsDbContext ctx)
    {
      _logger = logger;
      _ctx = ctx;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
      DomainEventsPublishTiming.Entities.User u1 = new("yzk");
      u1.ChangePassword("123456");
      u1.ChangeUserName("abc");
      _ctx.Add(u1);
      await _ctx.SaveChangesAsync();

      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }
  }
}