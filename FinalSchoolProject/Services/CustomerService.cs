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
            var allCustomers = await _dbContext.Customers.ToListAsync();
            var CustomerVMs = new List<CustomerWithAddressVM>();
            foreach (var customer in allCustomers) {
                CustomerVMs.Add(modelToVm(customer));
            }
            return CustomerVMs;
        }

        // Metoda uloží nového zákazníka + adresu
        public async Task<int> CreateCustomerAsync(CustomerWithAddressVM newCustomer) {
            var customer = VmToModel(newCustomer);
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
        // Je to mapovací metoda pro přenos dat do view
        // Budu muset nějak vybrat z kolekce tu správnou..
        // Není dokončeno!
        private CustomerWithAddressVM modelToVm(Customer customer) {
            return new CustomerWithAddressVM {
                Id = customer.Id,
                CompanyName = customer.CompanyName,
                Email = customer.Email,
                Phone = customer.Phone,
                CIN = customer.CIN,
                TIN = customer.TIN,
                RegistrationDate = customer.RegistrationDate,
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
                RegistrationDate = newCustomer.RegistrationDate,

            };
        }
    }
}
