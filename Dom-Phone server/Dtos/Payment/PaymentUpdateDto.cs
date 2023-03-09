using Dom_Phone_server.Models;

namespace Dom_Phone_server.Dtos.Payment
{
    public class PaymentUpdateDto
    {
        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
