using ECommerce.Models;
using ECommerce.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly IUnitOfWork _uow;
        public PaymentMethodController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> Index()
        {
            var paymentMethods = await _uow.Repository<PaymentMethod>().GetAllAsync();
            return View(paymentMethods);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                paymentMethod.Id = Guid.NewGuid();
                await _uow.Repository<PaymentMethod>().AddAsync(paymentMethod);
                await _uow.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(paymentMethod);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var paymentMethod = await _uow.Repository<PaymentMethod>().GetByIdAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return View(paymentMethod);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                _uow.Repository<PaymentMethod>().Update(paymentMethod);
                await _uow.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(paymentMethod);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var paymentMethod = await _uow.Repository<PaymentMethod>().GetByIdAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return View(paymentMethod);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paymentMethod = await _uow.Repository<PaymentMethod>().GetByIdAsync(id);
            await _uow.Repository<PaymentMethod>().Remove(paymentMethod);
            await _uow.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
