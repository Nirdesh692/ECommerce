using ECommerce.Models;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public async Task<IActionResult> Add(CartViewModel cart)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = cart.OrderId,
                PaymentMethodId = cart.PaymentMethodId,
            };
            await _paymentService.AddPaymentAsync(payment);
            return RedirectToAction("ShopView", "Shop");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
