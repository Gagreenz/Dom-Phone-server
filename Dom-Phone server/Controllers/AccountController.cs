using Dom_Phone_server.Services.TokenService.Interfaces;
using Dom_Phone_server.Services.AccountService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dom_Phone_server.Dtos.User;
using Dom_Phone_server.Models.Data;
using Dom_Phone_server.Dtos;
using System.Linq;
using Dom_Phone_server.Models;
using Microsoft.AspNetCore.Authorization;

namespace Dom_Phone_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        ITokenService _tokenService;
        public AccountController(IUserRepository userRepository,ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var serviceResponse = await _userRepository.Login(userLoginDto);

            if(!serviceResponse.IsSuccess)
            {
                return BadRequest(serviceResponse.Message);
            }

            await setRefreshToken(user: serviceResponse.Data!);
            setAccessToken(user: serviceResponse.Data!);

            return Ok("Success");
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var serviceResponse = await _userRepository.Register(userRegisterDto);
            
            if(!serviceResponse.IsSuccess)
            {
                return BadRequest(serviceResponse.Message);
            }

            await setRefreshToken(user: serviceResponse.Data!);
            setAccessToken(user: serviceResponse.Data!);

            return Ok("Success");
        }

        [HttpPost("resetRefreshToken")]
        public async Task<IActionResult> ResetRefreshToken()
        {
            var RefreshToken = Request.Cookies["RefreshToken"];

            if (RefreshToken == null) return BadRequest("Missing RefreshToken");

            var serviceResponse = await _userRepository.GetUserById(_tokenService.GetId(RefreshToken));
            if (!serviceResponse.IsSuccess) return BadRequest(serviceResponse!.Message);

            if (!_tokenService.VerifyRefreshToken(RefreshToken, serviceResponse.Data!)) return BadRequest("Wrong RefreshToken");

            await setRefreshToken(user: serviceResponse.Data!);
            setAccessToken(user: serviceResponse.Data!);

            return Ok("Success");
        }
        private async Task setRefreshToken(User user)
        {
            var refreshToken = _tokenService.GenerateRefreshToken(user);

            var RefreshCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddDays(30)
            };

            await _userRepository.SetRefreshToken(user, refreshToken);
            Response.Cookies.Append("refreshToken", refreshToken, RefreshCookieOptions);
        }
        private void setAccessToken(User user)
        {
            var accessToken = _tokenService.GenerateAccessToken(user);

            var AccessCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddMinutes(30)
            };

            Response.Cookies.Append("AccessToken", accessToken, AccessCookieOptions);

        }

    }
}
