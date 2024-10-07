using ECommerce.Models;
using ECommerce.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderStatusController : Controller
    {
        private readonly IUnitOfWork _uow;
        public OrderStatusController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> Index()
        {
            var orderStatuses = await _uow.Repository<OrderStatus>().GetAllAsync();
            return View(orderStatuses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                orderStatus.Id = Guid.NewGuid();
                await _uow.Repository<OrderStatus>().AddAsync(orderStatus);
                await _uow.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(orderStatus);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var orderStatus = await _uow.Repository<OrderStatus>().GetByIdAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }
            return View(orderStatus);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                _uow.Repository<OrderStatus>().Update(orderStatus);
                await _uow.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(orderStatus);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var orderStatus = await _uow.Repository<OrderStatus>().GetByIdAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }
            return View(orderStatus);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderStatus = await _uow.Repository<OrderStatus>().GetByIdAsync(id);
            await _uow.Repository<OrderStatus>().Remove(orderStatus);
            await _uow.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
