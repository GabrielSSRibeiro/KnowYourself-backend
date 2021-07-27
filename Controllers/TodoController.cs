using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        public IActionResult TestRun(){
            return Ok("success");
        }
    }
}
