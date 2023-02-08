using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dom_Phone_server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IHttpContextAccessor _accessor;
        public UserController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        [HttpGet]
        [Route("GetUserData")]
        [Authorize]
        public IActionResult GetUserData()
        {
            var text = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(text);
        }
    }
}
