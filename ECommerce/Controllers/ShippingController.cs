using ECommerce.Models;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Controllers
{
    public class ShippingController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IShippingService _shippingService;
        private readonly IShippingStatusService _shippingStatusService;
        public ShippingController(UserManager<User> userManager, IShippingService shippingService, IShippingStatusService shippingStatusService)
        {
            _userManager = userManager;
            _shippingService = shippingService;
            _shippingStatusService = shippingStatusService;
        } 
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CartViewModel cart )
        {
            if (ModelState.IsValid)
            {
                var shipping = new Shipping
                {
                    Id = Guid.NewGuid(),
                    Address = cart.Address
                };
                await _shippingService.AddShippingAsync(shipping);
                cart.ShippingId = shipping.Id;
                return RedirectToAction("CreateOrder", "Order", cart);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shipping = await _shippingService.GetAllShippingAsync();
            return View(shipping);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var shipping = await _shippingService.GetShippingByIdAsync(Id);
            ViewData["Shipping"] = new SelectList(await _shippingStatusService.GetAllShippingStatus(), "Id", "Status");
            return View(shipping);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Guid Id, Shipping shipping)
        {
            if (!ModelState.IsValid)
            {
                await _shippingService.UpdateShippingStatusAsync(Id, shipping);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
