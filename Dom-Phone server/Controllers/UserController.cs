using AutoMapper;
using Dom_Phone_server.Data;
using Dom_Phone_server.Dtos;
using Dom_Phone_server.Dtos.User;
using Dom_Phone_server.Services.AccountService.Interfaces;
using Dom_Phone_server.Services.PaymentService;
using Dom_Phone_server.Services.TokenService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dom_Phone_server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ITokenService _tokenService;
        IUserRepository _userRepository;
        IHttpContextAccessor _accessor;
        public UserController(
            ITokenService tokenService,
            IUserRepository userRepository,
            IHttpContextAccessor accessor,
            PaymentRepository paymentRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _accessor = accessor;
        }

        [HttpGet]
        [Route("GetUserData")]
        [Authorize]
        public async Task<ActionResult<UserInfoDto>> GetUserData()
        {
            string token = _accessor.HttpContext.Request.Headers["Authorization"];
            token = token.Substring("bearer ".Length).Trim();

            Guid userId = Guid.Parse(_tokenService.GetJwt(token).Claims.FirstOrDefault(c => c.Type == DomPhoneJWTClaims.UserId).Value);
            var serviceResponse = await _userRepository.GetInfo(userId);

            if (!serviceResponse.IsSuccess) return BadRequest(serviceResponse.Message);

            return Ok(serviceResponse.Data);
        }
        [HttpPut]
        [Route("UpdateInfo")]
        [Authorize]
        public async Task<ActionResult> UpdateInfo(UserUpdateDto userDto)
        {
            string token = _accessor.HttpContext.Request.Headers["Authorization"];
            token = token.Substring("bearer ".Length).Trim();

            Guid userId = Guid.Parse(_tokenService.GetJwt(token).Claims.FirstOrDefault(c => c.Type == DomPhoneJWTClaims.UserId).Value);
            var serviceResponse = await _userRepository.Update(userId, userDto);

            if (!serviceResponse.IsSuccess) return BadRequest(serviceResponse.Message);

            return Ok();
        }
        
    }
}
