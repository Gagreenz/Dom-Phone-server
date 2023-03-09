using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Dom_Phone_server.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public DateTime Date { get; set; } 

        public PaymentType Type { get; set; }
        public float Amount { get; set; }   
        public PaymentStatus Status { get; set; }
    }

    public enum PaymentType
    {
        [Description("Электричество")]
        Electricity,
        [Description("Водоснабжение")]
        Water
    }
    public enum PaymentStatus
    {
        [Description("Оплачено")]
        Confirm,
        [Description("Не оплачено")]
        Await,
        [Description("Просрочено")]
        Expired
    }
}
