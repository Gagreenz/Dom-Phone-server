using Dom_Phone_server.Models.Account;
using Dom_Phone_server.Services.AccountService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dom_Phone_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            _accountService.Login();

            return Ok("Успех");
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _accountService.Register(userData);
            
            if(user == null)
            {
                return BadRequest("User doesn`t created!");
            }
            
            return Ok("Успех");
        }

    }
}
