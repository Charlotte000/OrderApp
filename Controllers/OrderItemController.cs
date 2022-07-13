using Microsoft.AspNetCore.Mvc;
using OrderApp.Context;
using OrderApp.Models;

namespace OrderApp.Controllers
{
    public class OrderItemController : Controller
    {
        private ApplicationContext _db;

        public OrderItemController(ApplicationContext db)
        {
            this._db = db;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await this._db.OrderItems.FindAsync(id);
            if (item is null)
            {
                return BadRequest();
            }

            var orderId = item.OrderId;
            this._db.OrderItems.Remove(item);
            await this._db.SaveChangesAsync();
            return RedirectToAction("Index", "Order", new { id = orderId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(int orderId, OrderItem model)
        {
            model.OrderId = orderId;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await this._db.OrderItems.AddAsync(model);
            await this._db.SaveChangesAsync();
            return RedirectToAction("Index", "Order", new { id = orderId });
        }
    }
}
