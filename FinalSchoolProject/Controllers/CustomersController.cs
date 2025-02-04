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
            return RedirectToAction("Index");
        }
        // EDIT krok 1
        // zobrazí formulář s načtenými údaji konkrétního zákazníka
        public async Task<IActionResult> Edit(int id) {
            var customerToEdit = await _service.GetByIdAsync(id); //vyhledání zákazníka dle ID a uložení do proměnné
            if (customerToEdit == null) {
                return View("NotFound");
            }
            return View(customerToEdit);    // zobrazení údajů ve formuláři
        }
        // EDIT krok 4
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerWithAddressVM editedCustomer) {
            await _addressService.UpdateAddressAsync(id, editedCustomer);
            await _service.UpdateCustomerAsync(id, editedCustomer);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _addressService.DeleteAsync(id);
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
