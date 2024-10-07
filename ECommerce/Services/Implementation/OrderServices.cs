using AutoMapper;
using ECommerce.Helper;
using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace ECommerce.Services.Implementation
{
    public class OrderServices : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICartService _cartService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public OrderServices(IUnitOfWork uow, ICartService cartService, UserManager<User> userManager, IMapper mapper)
        {
            _uow = uow;
            _cartService = cartService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<Order> CreateOrderAsync(string userId, Guid shippingId)
        {
            var cart = await _uow.Repository<Cart>().Queryable().Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null || !cart.CartItems.Any(ci => (bool)ci.Selected))
            {
                throw new Exception("Cart is empty or no items selected.");
            }
            var selectedItems = cart.CartItems.Where(ci => (bool)ci.Selected).ToList();
            var orderStatus = await _uow.Repository<OrderStatus>().Queryable().FirstOrDefaultAsync(os=>os.Status == "Pending");
            var shipping = await _uow.Repository<Shipping>().GetByIdAsync(shippingId);
            var shippingStatus = await _uow.Repository<ShippingStatus>().GetByIdAsync((Guid)shipping.ShippingStatusId);
            var paymentStatuses = await _uow.Repository<PaymentStatus>().Queryable().FirstOrDefaultAsync(ps => ps.Status == "Unpaid");

            double netAmount = cart.GrandTotal;
            double taxAmount = 13 / 100 * cart.GrandTotal;
            double total = netAmount + taxAmount;
            double discount = 10 / 100 * total;
            double grossTotal = total - discount;
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrderDate = DateTime.Now,
                NetAmount = netAmount,
                TaxAmount = taxAmount,
                Discount = discount,
                GrossAmount = grossTotal,
                Shipping = shipping,

                OrderItems = selectedItems.Select(ci => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice,
                    ProductId = ci.ProductId,
                }).ToList(),
                OrderStatusId = orderStatus.Id,
                ShippingId = shippingId
                
            };
            foreach(var item in selectedItems)
            {
                var product = item.Product;
                product.StockQuantity -= item.Quantity;
                _uow.Repository<Product>().Update(product);
            }
            await _uow.Repository<Order>().AddAsync(order);
            await _cartService.ClearCartAsync(userId);
            await _uow.CompleteAsync();
            return order;
        }
        public async Task<PaginatedList<Order>> GetOrderByUserIdAsync(string userId, int pageNumber, int pageSize)
        {
            var orders = _uow.Repository<Order>().Queryable()
                                                 .Where(o => o.UserId == userId)
                                                 .Include(o => o.OrderStatus)
                                                 .Include(o => o.User)
                                                 .Include(o=>o.Shipping)
                                                 .OrderByDescending(o => o.OrderDate);

            var totalRecords = await orders.CountAsync();
            var paginatedOrders = orders
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();
            var OrderList = new PaginatedList<Order>(paginatedOrders, totalRecords, pageNumber, pageSize);
            return OrderList;
        }
        public async Task<PaginatedList<Order>> GetAllOrdersAsync(int pageNumber, int pageSize)
        {
            var orders = _uow.Repository<Order>().Queryable()
                                                 .Include(o=>o.OrderStatus)
                                                 .Include(o=>o.User)
                                                 .OrderByDescending(o=>o.OrderDate);
            
            var totalRecords = orders.Count();
            var paginatedOrders = orders
                                    .OrderByDescending(o => o.OrderDate)
                                    .Skip((pageNumber -1)*pageSize)
                                    .Take(pageSize)
                                    .ToList();
            return new PaginatedList<Order>(paginatedOrders, totalRecords, pageNumber, pageSize);
        }
        public async Task<OrderViewModel> GetOrderByIdAsync(Guid id)
        {
            var order = await _uow.Repository<Order>().Queryable()
                                                      .Include(o => o.User)
                                                      .Include(o => o.OrderItems)
                                                      .ThenInclude(oi => oi.Products)
                                                      .Include(o => o.OrderStatus)
                                                      .Include(o => o.Shipping)
                                                      .FirstOrDefaultAsync(o => o.Id == id);

            var orderItemView = _mapper.Map<IEnumerable<OrderItemViewModel>>(order.OrderItems);

            var orderView = _mapper.Map<OrderViewModel>(order);
            orderView.OrderItems = orderItemView;

            return orderView;
        } 
        public async Task UpdateOrderStatusAsync(Guid orderId, Guid OrderStatusId)
        {
            var order = await _uow.Repository<Order>().GetByIdAsync(orderId);
            order.OrderStatusId=OrderStatusId;
            _uow.Repository<Order>().Update(order);
            await _uow.CompleteAsync();
        }
    }
}
