using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiResilienceWithPolly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> CallApiExterne()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("https://localhost:7286/WeatherForecast/50");

            return response.IsSuccessStatusCode ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
