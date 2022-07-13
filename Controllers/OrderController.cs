using Microsoft.AspNetCore.Mvc;
using OrderApp.Context;
using OrderApp.Models;

namespace OrderApp.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationContext _db;

        public OrderController(ApplicationContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var order = await this._db.Orders.FindAsync(id);
            if (order is null)
            {
                return BadRequest();
            }

            ViewBag.Order = order;
            ViewBag.OrderItems = this._db.OrderItems.Where(item => item.OrderId == order.Id);
            ViewBag.Providers = this._db.Providers.ToArray();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await this._db.Orders.AddAsync(model);
            await this._db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await this._db.Orders.FindAsync(id);
            if (order is null)
            {
                return BadRequest();
            }

            // Delete order items
            foreach (var item in this._db.OrderItems.Where(item => item.OrderId == order.Id))
            {
                this._db.OrderItems.Remove(item);
            }

            // Delete order
            this._db.Orders.Remove(order);

            await this._db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            this._db.Orders.Update(model);
            await this._db.SaveChangesAsync();
            return RedirectToAction("Index", "Order", new { id = model.Id });
        }
    }
}
