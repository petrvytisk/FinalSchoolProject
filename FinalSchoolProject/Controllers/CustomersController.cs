using Microsoft.AspNetCore.Mvc;

namespace FinalSchoolProject.Controllers {
    public class CustomersController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
