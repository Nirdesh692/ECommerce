using ECommerce.Services.Implementation;
using ECommerce.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IOrderService _orderService;
        public InvoiceController(IInvoiceService invoiceService, IOrderService orderService)
        {
            _invoiceService = invoiceService;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var invoices = await _invoiceService.GetAllInvoiceAsync();
            return View(invoices);
        }

    }
}
