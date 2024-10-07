namespace ECommerce.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public int InvoiceNumber { get; set; }
        public DateOnly InvoiceDate { get; set; }
        public double NetAmount { get; set; }
        public double GrossAmount { get; set; }
        public double Discount { get; set; }
        public double TaxAmount { get; set; }
        public double ShippingCharges { get; set; }

        public Guid? ShippingId { get; set; }
        public Shipping? Shipping { get; set; }
        
        public Guid? PaymentId { get; set; }
        public Payment? Payment { get; set; }
        
        public Guid? OrderId {  get; set; }
        public Order? Order { get; set; }
    }
}
