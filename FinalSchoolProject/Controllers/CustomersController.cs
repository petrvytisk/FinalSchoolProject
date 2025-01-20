using FinalSchoolProject.DTO;
using FinalSchoolProject.Models;
using FinalSchoolProject.Services;
using FinalSchoolProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalSchoolProject.Controllers {
    public class CustomersController : Controller {
        private CustomerService _service;
        private AddressService _addressService;

        public CustomersController(CustomerService customerService, AddressService addressService) {
            _service = customerService;
            _addressService = addressService;
        }

        // PŮVODNÍ ACTION
        //public IActionResult Index() {
        //    return View();
        //}

        // zobrazí stránku, kde se zobrazí všichni zákazníci
        public async Task<IActionResult> Index() {
            var allCustomers = await _service.GetAllCustomersAsync();
            return View(allCustomers);
        }
        // zobrazí formulář pro vložení zákazníka + adresy
        public IActionResult Create() {
            return View();
        }
        // uloží nového zákazníka + adresu do databáze
        [HttpPost]
        public async Task<IActionResult> Create(CustomerWithAddressVM newCustomer) {
            // Uložíme zákazníka a získáme jeho ID
            int customerId = await _service.CreateCustomerAsync(newCustomer);
            // Uložíme adresu zákazníka a získáme ID adresy
            int addressId = await _addressService.CreateAddressAsync(newCustomer, customerId);
            // Aktualizujeme zákazníka s ID adresy
            await _service.UpdateCustomerAddressId(customerId, addressId);
            //return RedirectToAction("Index"); // pokus
            return RedirectToAction("Index", "Orders");
        }
    }
}
