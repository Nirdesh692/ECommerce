using ECommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ShippingStatus> ShippingsStatus { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);
            builder.Entity<Cart>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Cart>(u => u.UserId);
            builder.Entity<CartItem>()
                .HasOne(c => c.Cart)
                .WithMany(ci => ci.CartItems)
                .HasForeignKey(c => c.CartId);
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(u => u.UserId);
            builder.Entity<Payment>()
                .HasOne(o=>o.Order)
                .WithMany()
                .HasForeignKey(o => o.OrderId);
            builder.Entity<Order>()
                .HasOne(os => os.OrderStatus)
                .WithMany()
                .HasForeignKey(o => o.OrderStatusId);
            builder.Entity<Order>()
                .HasOne(s=>s.Shipping)
                .WithMany()
                .HasForeignKey(o => o.ShippingId);
            builder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(o => o.OrderId);
            builder.Entity<OrderItem>()
                .HasOne(p => p.Products)
                .WithMany()
                .HasForeignKey(p => p.ProductId);
            builder.Entity<Invoice>()
                .HasOne(s => s.Shipping)
                .WithMany()
                .HasForeignKey(s => s.ShippingId);
            builder.Entity<Invoice>()
                .HasOne(p => p.Payment)
                .WithOne()
                .HasForeignKey<Invoice>(p => p.PaymentId);
            builder.Entity<Invoice>()
                .HasOne(o => o.Order)
                .WithMany()
                .HasForeignKey(o => o.OrderId);
            builder.Entity<Payment>()
                .HasOne(p => p.PaymentStatus)
                .WithMany()
                .HasForeignKey(p => p.PaymentStatusId);
            builder.Entity<Shipping>()
                .HasOne(s => s.ShippingStatus)
                .WithMany()
                .HasForeignKey(s=>s.ShippingStatusId);
            builder.Entity<Review>()
                .HasOne(u => u.User)
                .WithMany(r=>r.Reviews)
                .HasForeignKey(u => u.UserId);
            builder.Entity<Review>()
                .HasOne(p=>p.Product)
                .WithMany(r=>r.Reviews)
                .HasForeignKey(p=>p.ProductId);

            base.OnModelCreating(builder);
        }
        public DbSet<PaymentMethod> PaymentMethod { get; set; } 
    }
}