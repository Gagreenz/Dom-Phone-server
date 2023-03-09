using Dom_Phone_server.Data;
using Dom_Phone_server.Dtos.Payment;
using Dom_Phone_server.Services.AccountService.Interfaces;
using Dom_Phone_server.Services.PaymentService;
using Dom_Phone_server.Services.TokenService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dom_Phone_server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        PaymentRepository _paymentRepository;

        ITokenService _tokenService;
        IHttpContextAccessor _accessor;
        public PaymentController(
            ITokenService tokenService,
            IHttpContextAccessor accessor,
            PaymentRepository paymentRepository)
        {
            _tokenService = tokenService;
            _accessor = accessor;
            _paymentRepository = paymentRepository;
        }

        [HttpPost]
        [Route("UppdatePayment")]
        [Authorize]
        public IActionResult UppdatePayment(PaymentUpdateDto paymentDto)
        {
            _paymentRepository.UpdatePayment(paymentDto);
            return Ok();
        }

        [HttpPost]
        [Route("AddPayment")]
        [Authorize]
        public IActionResult AddPayment(PaymentCreateDto paymentDto)
        {
            string token = _accessor.HttpContext.Request.Headers["Authorization"];
            token = token.Substring("bearer ".Length).Trim();

            Guid userId = Guid
                .Parse(_tokenService.GetJwt(token).Claims
                .FirstOrDefault(c => c.Type == DomPhoneJWTClaims.UserId).Value);

            _paymentRepository.AddPayment(userId,paymentDto);
            return Ok();
        }

        [HttpGet]
        [Route("GetAllPayments")]
        [Authorize]
        public ActionResult<PaymentDto> GetAllPayments()
        {
            string token = _accessor.HttpContext.Request.Headers["Authorization"];
            token = token.Substring("bearer ".Length).Trim();

            Guid userId = Guid.Parse(_tokenService.GetJwt(token).Claims.FirstOrDefault(c => c.Type == DomPhoneJWTClaims.UserId).Value);
            var response = _paymentRepository.GetAllPayments(userId);

            return Ok(response);
        }
    }
}
