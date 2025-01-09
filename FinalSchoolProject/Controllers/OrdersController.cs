using FinalSchoolProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalSchoolProject.Controllers {
    public class OrdersController : Controller {
        private OrderService orderService;

        public OrdersController(OrderService orderService) {
            this.orderService = orderService;
        }

        public async Task<ActionResult> Index() {
            var allOrders = await orderService.GetAllOrdersAsync();
            return View(allOrders);
        }

        public IActionResult Create() {
            return View();
        }
    }
}
