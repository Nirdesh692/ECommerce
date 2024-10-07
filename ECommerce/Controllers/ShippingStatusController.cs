using ECommerce.Models;
using ECommerce.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShippingStatusController : Controller
    {
        private readonly IUnitOfWork _uow;
        public ShippingStatusController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> Index()
        {
            var shippingStatuses = await _uow.Repository<ShippingStatus>().GetAllAsync();
            return View(shippingStatuses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ShippingStatus shippingStatus)
        {
            if (ModelState.IsValid)
            {
                shippingStatus.Id = Guid.NewGuid();
                await _uow.Repository<ShippingStatus>().AddAsync(shippingStatus);
                await _uow.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(shippingStatus);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var shippingStatus = await _uow.Repository<ShippingStatus>().GetByIdAsync(id);
            if (shippingStatus == null)
            {
                return NotFound();
            }
            return View(shippingStatus);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ShippingStatus shippingStatus)
        {
            if (ModelState.IsValid)
            {
                _uow.Repository<ShippingStatus>().Update(shippingStatus);
                await _uow.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(shippingStatus);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var shippingStatus = await _uow.Repository<ShippingStatus>().GetByIdAsync(id);
            if (shippingStatus == null)
            {
                return NotFound();
            }
            return View(shippingStatus);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shippingStatus = await _uow.Repository<ShippingStatus>().GetByIdAsync(id);
            await _uow.Repository<ShippingStatus>().Remove(shippingStatus);
            await _uow.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
