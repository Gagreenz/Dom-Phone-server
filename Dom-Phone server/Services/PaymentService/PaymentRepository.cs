using AutoMapper;
using Dom_Phone_server.Dtos.Payment;
using Dom_Phone_server.Models;
using Dom_Phone_server.Models.Data;
using Dom_Phone_server.Models.DB;

namespace Dom_Phone_server.Services.PaymentService
{
    public class PaymentRepository
    {
        private readonly UserContext _context;
        IMapper _mapper;
        public PaymentRepository(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void AddPayment(Guid userId,PaymentCreateDto paymentDto)
        {
            var payment = _mapper.Map<PaymentCreateDto, Payment>(paymentDto);
            payment.UserId = userId;

            _context.Payments.Add(payment);
            _context.SaveChanges();
        }
        public void UpdatePayment(PaymentUpdateDto paymentDto)
        {
            Payment existingPayment = _context.Payments.FirstOrDefault(p => p.Id == paymentDto.Id);

            
        }
        public List<PaymentDto> GetAllPayments(Guid userId)
        {
            var payments = _context.Payments.Where(x => x.UserId == userId).ToList();

            List<PaymentDto> result = new List<PaymentDto>();
            foreach(var payment in payments)
            {
                result.Add(_mapper.Map<Payment, PaymentDto>(payment));
            }


            return result;
        }
    }
}
