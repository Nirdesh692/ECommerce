namespace ECommerce.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public int Ratings { get; set; }
        public string Comment { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public string? UserId { get; set; }
        public User? User {  get; set; }
        
    }
}
