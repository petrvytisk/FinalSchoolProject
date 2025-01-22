using FinalSchoolProject.Models;
using FinalSchoolProject.DTO;
using FinalSchoolProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalSchoolProject.Services {
    public class CustomerService {
        private ApplicationDbContext _dbContext;

        public CustomerService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
        // VÝPIS ZÁZNAMŮ
        // Metoda načte všechny zákazníky a vrátí je ve formě viewmodelu
        public async Task<IEnumerable<CustomerWithAddressVM>> GetAllCustomersAsync() {
            var allCustomers = await _dbContext.Customers.Include(c => c.Address).ToListAsync();    // Zahrnutí adresy

            var customerVMs = new List<CustomerWithAddressVM>();
            foreach (var customer in allCustomers) {
                customerVMs.Add(modelToVm(customer));
            }
            return customerVMs;
        }

        // Metoda uloží nového zákazníka + adresu
        public async Task<int> CreateCustomerAsync(CustomerWithAddressVM newCustomer) {
            var customer = VmToModel(newCustomer);
            // Nastavení aktuálního data registrace
            customer.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer.Id; // Vrátí skutečné ID vytvořeného zákazníka
        }

        public async Task UpdateCustomerAddressId(int customerId, int addressId) {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer != null) {
                customer.AddressId = addressId; // Nastavení AddressId
                await _dbContext.SaveChangesAsync();
            }
        }

        // Mapovací metoda MODEL => VIEWMODEL
        private CustomerWithAddressVM modelToVm(Customer customer) {
            return new CustomerWithAddressVM {
                Id = customer.Id,
                CompanyName = customer.CompanyName,
                Email = customer.Email,
                Phone = customer.Phone,
                CIN = customer.CIN,
                TIN = customer.TIN,
                RegistrationDate = customer.RegistrationDate,
                Street = customer.Address?.Street,   // Kontrola null
                City = customer.Address?.City,
                PostalCode = customer.Address?.PostalCode,
                HouseNumber = customer.Address?.HouseNumber,
                StreetNumber = customer.Address?.StreetNumber,
                Country = customer.Address?.Country,
                Region = customer.Address?.Region,
            };
        }

        // Mapovací metoda VIEWMODEL => MODEL
        private Customer VmToModel(CustomerWithAddressVM newCustomer) {
            return new Customer {
                CompanyName = newCustomer.CompanyName,
                Email = newCustomer.Email,
                Phone = newCustomer.Phone,
                CIN = newCustomer.CIN,
                TIN = newCustomer.TIN,
            };
        }
    }
}
