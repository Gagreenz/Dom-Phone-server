using Dom_Phone_server.Models;

namespace Dom_Phone_server.Dtos.Payment
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public PaymentType Type { get; set; }
        public float Amount { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
