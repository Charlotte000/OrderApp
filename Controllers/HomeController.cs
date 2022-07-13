using Microsoft.AspNetCore.Mvc;
using OrderApp.Context;
using OrderApp.Models;
using System.Diagnostics;

namespace OrderApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _db;

        public HomeController(ApplicationContext _db)
        {
            this._db = _db;
        }

        public IActionResult Index(Filter filter)
        {
            ViewBag.Orders = filter.Apply(this._db.Orders.AsQueryable(), this._db.OrderItems.AsQueryable()).ToArray();
            ViewBag.Providers = this._db.Providers.ToArray();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}