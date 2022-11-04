using Microsoft.AspNetCore.Mvc;

namespace ApiRessourcesExternes.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [Route("{count}")]
        [HttpGet]
        public ActionResult<WeatherForecast> Get(int count)
        {
            Random random = new Random();
            var result = random.Next(0, 100);
            
            if (result>count)
            {
                return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
            .ToArray());
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}