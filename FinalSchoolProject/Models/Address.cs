using System.IO;

namespace FinalSchoolProject.Models {
    public class Address {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;  //  ulice
        public string City { get; set; } = string.Empty;    // město
        public string PostalCode { get; set; } = string.Empty;  // PSČ
        public string HouseNumber { get; set; } = string.Empty; // číslo orientační
        public string ApartmentNumber { get; set; } = string.Empty; //  číslo popisné
        public string Country { get; set; } = string.Empty; //  Stát
        public string Region { get; set; } = string.Empty;  //  Kraj (oblast)
        public Customer Customer { get; set; }
    }
}