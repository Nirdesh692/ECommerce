using ECommerce.Models;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderStatusService _orderStatusService;
        private readonly UserManager<User> _userManager;
        
        public OrderController(IOrderService orderService, IOrderStatusService orderStatusService, UserManager<User> userManager)
        {
            _orderService = orderService;
            _orderStatusService = orderStatusService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var order = await _orderService.GetAllOrdersAsync(pageNumber, pageSize);
            var orderStatusList = await _orderStatusService.GetAllOrderStatusAsync();
            ViewData["orderStatusId"] = new SelectList(orderStatusList, "Id", "Status");
            return View(order);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            ViewData["orderStatusId"] = new SelectList(await _orderStatusService.GetAllOrderStatusAsync(), "Id", "Status");

            return View(order);
        }
        public async Task<IActionResult> CreateOrder(CartViewModel cart, int pageNumber=1, int pageSize=10)
        {
            var user = await _userManager.GetUserAsync(User);
            await _orderService.CreateOrderAsync(user.Id, cart.ShippingId);
            var orders = await _orderService.GetOrderByUserIdAsync(user.Id, pageNumber, pageSize);
            var order = orders.LastOrDefault();
            cart.OrderId = order.Id;
            return RedirectToAction("Add", "Payment", cart);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus([FromBody]Order order)
        {
            await _orderService.UpdateOrderStatusAsync(order.Id, (Guid)order.OrderStatusId);
            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
