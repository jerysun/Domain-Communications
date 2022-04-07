using System.Reflection;
using Zack.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string rabbitUrl = "YOUR-RABBIT-URL";
const string queueName = "queue1";
const string exchangeName = "exchangeEventBusDemo1";

builder.Services.Configure<IntegrationEventRabbitMQOptions>(o => {
  o.RabbitUrl = rabbitUrl;
  o.ExchangeName = exchangeName;
});
builder.Services.AddEventBus(queueName, Assembly.GetExecutingAssembly());

var app = builder.Build();
app.UseEventBus();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
