using Microsoft.AspNetCore.Mvc;

namespace Tekus.API.Controllers;

public class HealthController : ApiControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
    }
}
