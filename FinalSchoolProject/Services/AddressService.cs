using FinalSchoolProject.Models;
using FinalSchoolProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalSchoolProject.Services {
    public class AddressService {
        private ApplicationDbContext _dbContext;

        public AddressService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        // Metoda uloží novou adresu zákazníka
        public async Task<int> CreateAddressAsync(CustomerWithAddressVM newAddress, int customerId) {
            var address = VmToModel(newAddress, customerId);
            await _dbContext.Addresses.AddAsync(address);
            await _dbContext.SaveChangesAsync();
            return address.Id;
        }
        // EDIT krok 8
        // Mapovací metoda VIEWMODEL => MODEL
        private Address VmToModel(CustomerWithAddressVM newAddress, int customerId) {
            return new Address {
                Id = newAddress.AddressId,  //PŘIDÁNO
                Street = newAddress.Street,
                City = newAddress.City,
                PostalCode = newAddress.PostalCode,
                HouseNumber = newAddress.HouseNumber,
                StreetNumber = newAddress.StreetNumber,
                Country = newAddress.Country,
                Region = newAddress.Region,
                CustomerId = customerId,    // toto by se mohlo načíst i z viewmodelu
            };
        }
        // EDIT krok 7
        public async Task<CustomerWithAddressVM> UpdateAddressAsync(int id, CustomerWithAddressVM updatedCustomer) {
            Address? existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.CustomerId == id);
            existingAddress.Customer = await _dbContext.Customers.FirstOrDefaultAsync(a => a.Id == updatedCustomer.Id);
            existingAddress.Street = updatedCustomer.Street;
            existingAddress.City = updatedCustomer.City;
            existingAddress.PostalCode = updatedCustomer.PostalCode;
            existingAddress.HouseNumber = updatedCustomer.HouseNumber;
            existingAddress.Country = updatedCustomer.Country;
            existingAddress.Region = updatedCustomer.Region;
            existingAddress.CustomerId = updatedCustomer.Id;

            _dbContext.Addresses.Update(existingAddress);
            await _dbContext.SaveChangesAsync();    //tímto je adresa zákazníka aktualizována
            return updatedCustomer;     // je nutné toto vracet??
        }

        public async Task DeleteAsync(int id) {
            var addressToDelete = await _dbContext.Addresses.FirstOrDefaultAsync(c => c.CustomerId == id);
            _dbContext.Addresses.Remove(addressToDelete);
            _dbContext.SaveChanges();
        }
    }
}
