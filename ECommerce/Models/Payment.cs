namespace ECommerce.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid? PaymentStatusId { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
    }
}
