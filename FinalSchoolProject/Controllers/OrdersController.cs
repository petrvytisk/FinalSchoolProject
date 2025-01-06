using FinalSchoolProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalSchoolProject.Controllers {
    public class OrdersController : Controller {
        private OrderService orderService;

        public OrdersController(OrderService orderService) {
            this.orderService = orderService;
        }

        public IActionResult Index() {
            var allOrders = orderService.GetAllOrders();
            return View(allOrders);
        }
    }
}
