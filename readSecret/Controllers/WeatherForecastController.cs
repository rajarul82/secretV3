using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;

namespace readSecret.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SecretClient _secretClient;

        public WeatherForecastController(SecretClient secretClient, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _secretClient = secretClient;
        }

        [HttpGet]
        public async void ReadSecreKey()
        {
            _logger.LogInformation($"begin secret value from azure");
            var secretValue = await _secretClient.GetSecretAsync("myKey");
            _logger.LogInformation($"secret value from azure - {secretValue.Value.Value}");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}