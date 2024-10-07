using ECommerce.Models;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orderService;
        private readonly IOrderStatusService _orderStatusService;
        public ProfileController(UserManager<User> userManager, IOrderService orderService, IOrderStatusService orderStatusService)
        {
            _userManager = userManager;
            _orderService = orderService;
            _orderStatusService = orderStatusService;
        }
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };
            return View(profile);
        }
        public async Task<IActionResult> Orders(int pageNumber = 1, int pageSize = 10)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null) { return RedirectToAction("Index", "Home"); }
            var orders = await _orderService.GetOrderByUserIdAsync(user.Id, pageNumber, pageSize);
            ViewData["orderStatusId"] = new SelectList(await _orderStatusService.GetAllOrderStatusAsync(), "Id", "Status");
            return View(orders);
        }
    }
}
