using FinalSchoolProject.DTO;
using FinalSchoolProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalSchoolProject.Controllers {
    public class OrdersController : Controller {
        private OrderService _service;

        public OrdersController(OrderService orderService) {
            this._service = orderService;
        }
        // zobrazí stránku, kde se zobrazí všechny objednávky
        public async Task<IActionResult> Index() {
            var allOrders = await _service.GetAllOrdersAsync();
            return View(allOrders);
        }
        // zobrazí formulář pro vložení objednávky
        public IActionResult Create() {
            return View();
        }
        // uloží novou objednávku do databáze
        [HttpPost]
        public async Task<IActionResult> Create(OrderDTO newOrder) {
            await _service.CreateAsync(newOrder);
            return RedirectToAction("Index");
        }
    }
}
