using FinalSchoolProject.Models;
using FinalSchoolProject.ViewModels;

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

        // Mapovací metoda VIEWMODEL => MODEL
        private Address VmToModel(CustomerWithAddressVM newAddress, int customerId) {
            return new Address {
                Street = newAddress.Street,
                City = newAddress.City,
                PostalCode = newAddress.PostalCode,
                HouseNumber = newAddress.HouseNumber,
                StreetNumber = newAddress.StreetNumber,
                Country = newAddress.Country,
                Region = newAddress.Region,
                CustomerId = customerId,
            };
        }
    }
}
