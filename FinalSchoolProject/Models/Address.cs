using System.IO;

namespace FinalSchoolProject.Models {
    public class Address {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;  //  ulice
        public string City { get; set; } = string.Empty;    // město
        public string PostalCode { get; set; } = string.Empty;  // PSČ
        public string HouseNumber { get; set; } = string.Empty; // číslo popisné
        public string StreetNumber { get; set; } = string.Empty; //  číslo orientační
        public string Country { get; set; } = string.Empty; //  stát
        public string Region { get; set; } = string.Empty;  //  kraj (oblast)
        // Navigační vlastnost na Customer
        public Customer? Customer { get; set; }
        // Nepovinný cizí klíč (volitelné, ale doporučené pro explicitnost) - lze pak jednosuše zahrnou pomocí ".Include"
        public int? CustomerId { get; set; }
    }
}