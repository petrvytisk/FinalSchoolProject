using FinalSchoolProject.DTO;
using FinalSchoolProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

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
        public async Task<IActionResult> Create() {
            var ordersDropdownData = await _service.GetNewOrdersDropdownValues();   // získání dat pro select  
            ViewBag.Customers = new SelectList(ordersDropdownData.Customers, "Id", "CompanyName");
            ViewBag.Statuses = new SelectList(ordersDropdownData.Statuses, "Id", "Name");
            return View();
        }

        // uloží novou objednávku do databáze
        [HttpPost]
        public async Task<IActionResult> Create(OrderDTO newOrder) {
            decimal totalPriceDecimal;
       
            // Převod TotalPrice z řetězce na decimal
            // Když vstupní data neprojdou validací (nebo se číslo nepřevede na decimal), akce se opakuje.
            // Zadané údaje zůstanou vyplněny
            if ((!ModelState.IsValid) || (!decimal.TryParse(newOrder.TotalPrice, NumberStyles.Any, new CultureInfo("cs-CZ"), out totalPriceDecimal))) {
                var ordersDropdownData = await _service.GetNewOrdersDropdownValues();   // získání dat pro select
                ViewBag.Customers = new SelectList(ordersDropdownData.Customers, "Id", "CompanyName");
                ViewBag.Statuses = new SelectList(ordersDropdownData.Statuses, "Id", "Name");
                return View(newOrder);
            }
            await _service.CreateAsync(newOrder, totalPriceDecimal);
            return RedirectToAction("Index");
        }
        // uloží novou objednávku do databáze    PŮVODNÍ MOJE
        //[HttpPost]
        //public async Task<IActionResult> Create(OrderDTO newOrder) {
        //    await _service.CreateAsync(newOrder);
        //    return RedirectToAction("Index");
        //}
    }
}
