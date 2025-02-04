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
                customerVMs.Add(ModelToVm(customer));
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

        // EDIT krok 2
        // vytáhne z databáze konkrétního zákazníka dle ID (přidá k němu i jeho adresu)
        public async Task<CustomerWithAddressVM> GetByIdAsync(int id) {
            var customer = await _dbContext.Customers.Include(c=>c.Address).FirstOrDefaultAsync(c => c.Id ==
            id);
            return ModelToVm(customer); // vrátí údaje ve formě ViewModelu
        }

        // EDIT krok 3
        // Mapovací metoda MODEL => VIEWMODEL
        private CustomerWithAddressVM ModelToVm(Customer customer) {
            return new CustomerWithAddressVM {
                Id = customer.Id,
                CompanyName = customer.CompanyName,
                Email = customer.Email,
                Phone = customer.Phone,
                CIN = customer.CIN,
                TIN = customer.TIN,
                RegistrationDate = customer.RegistrationDate,
                AddressId = customer.AddressId, //PŘIDÁNO
                Street = customer.Address?.Street,   // Kontrola null
                City = customer.Address?.City,
                PostalCode = customer.Address?.PostalCode,
                HouseNumber = customer.Address?.HouseNumber,
                StreetNumber = customer.Address?.StreetNumber,
                Country = customer.Address?.Country,
                Region = customer.Address?.Region,
            };
        }
        // EDIT krok 6
        // Mapovací metoda VIEWMODEL => MODEL
        private Customer VmToModel(CustomerWithAddressVM newCustomer) {
            return new Customer {
                Id = newCustomer.Id,    //PŘIDÁNO pro EDIT - je třeba zjistit jak se to zachová při novém zákazníkovi
                CompanyName = newCustomer.CompanyName,
                Email = newCustomer.Email,
                Phone = newCustomer.Phone,
                CIN = newCustomer.CIN,
                TIN = newCustomer.TIN,
            };
        }
        // EDIT krok 5
        // ID se ve výsledku ani nevyužije, dalo by se ale udělat ověření, zda IDčka souhlasí
        public async Task<CustomerWithAddressVM> UpdateCustomerAsync(int id, CustomerWithAddressVM updatedCustomer) {
            Customer? existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            existingCustomer.Address = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.CustomerId == id);
            existingCustomer.CompanyName = updatedCustomer.CompanyName;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Phone = updatedCustomer.Phone;
            existingCustomer.CIN = updatedCustomer.CIN;
            existingCustomer.TIN = updatedCustomer.TIN;           
            _dbContext.Customers.Update(existingCustomer);
            await _dbContext.SaveChangesAsync();    // tímto je zákazník aktualizován
            return updatedCustomer;     // je nutné toto vracet??
        }

        public async Task DeleteAsync(int id) {
            var customerToDelete = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            _dbContext.Customers.Remove(customerToDelete);
            _dbContext.SaveChanges();
        }
    }
}
