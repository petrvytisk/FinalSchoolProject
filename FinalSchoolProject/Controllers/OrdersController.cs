using Microsoft.AspNetCore.Mvc;

namespace FinalSchoolProject.Controllers {
    public class OrdersController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
