using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowAllOrigins")]
        public IActionResult Get()
        {
            return Ok(new { Message = "Hello, World!" });
        }
    }
}