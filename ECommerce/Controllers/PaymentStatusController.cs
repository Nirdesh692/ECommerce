using ECommerce.Models;
using ECommerce.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PaymentStatusController : Controller
    {
        private readonly IUnitOfWork _uow;
        public PaymentStatusController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> Index()
        {
            var paymentStatuses = await _uow.Repository<PaymentStatus>().GetAllAsync();
            return View(paymentStatuses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PaymentStatus paymentStatus)
        {
            if(ModelState.IsValid)
            {
                paymentStatus.Id = Guid.NewGuid();
                await _uow.Repository<PaymentStatus>().AddAsync(paymentStatus);
                await _uow.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(paymentStatus);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var paymentStatus = await _uow.Repository<PaymentStatus>().GetByIdAsync(id);
            if(paymentStatus == null)
            {
                return NotFound();
            }
            return View(paymentStatus);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, PaymentStatus paymentStatus)
        {
            if(ModelState.IsValid)
            {
                _uow.Repository<PaymentStatus>().Update(paymentStatus);
                await _uow.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(paymentStatus);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var paymentStatus = await _uow.Repository<PaymentStatus>().GetByIdAsync(id);
            if (paymentStatus == null)
            {
                return NotFound();
            }
            return View(paymentStatus);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paymentStatus = await _uow.Repository<PaymentStatus>().GetByIdAsync(id);
            await _uow.Repository<PaymentStatus>().Remove(paymentStatus);
            await _uow.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
