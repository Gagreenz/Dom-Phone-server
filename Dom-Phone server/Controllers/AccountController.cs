using Dom_Phone_server.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Dom_Phone_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok("Успех");
        }

    }
}
