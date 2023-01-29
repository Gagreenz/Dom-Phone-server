using Dom_Phone_server.Models.Account;
using Dom_Phone_server.Services.TokenService.Interfaces;
using Dom_Phone_server.Services.AccountService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dom_Phone_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        public AccountController(IAccountService accountService,ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _accountService.LoginAsync(userLoginDto);

            if(user == null)
            {
                return BadRequest("Login has failed");
            }
            setRefreshToken(user);

            return Ok("Успех");
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var user = await _accountService.RegisterAsync(userRegisterDto);
            
            if(user == null)
            {
                return BadRequest("User doesn`t created!");
            }

            setRefreshToken(user);

            return Ok("Успех");
        }

        private void setRefreshToken(User user)
        {
            var refreshToken = _tokenService.GenerateRefreshToken(user);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiredAt
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
            _accountService.SetRefreshToken(refreshToken, user);
        }

    }
}
