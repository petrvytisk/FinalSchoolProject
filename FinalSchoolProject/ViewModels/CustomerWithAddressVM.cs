namespace FinalSchoolProject.ViewModels {
    public class CustomerWithAddressVM {
        // Zákazník
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CIN { get; set; } = string.Empty; // IČO
        public string TIN { get; set; } = string.Empty; // DIČ
        public DateOnly RegistrationDate { get; set; }
        // Adresa
        public string Street { get; set; } = string.Empty;  //  ulice
        public string City { get; set; } = string.Empty;    // město
        public string PostalCode { get; set; } = string.Empty;  // PSČ
        public string HouseNumber { get; set; } = string.Empty; // číslo orientační
        public string ApartmentNumber { get; set; } = string.Empty; //  číslo popisné
        public string Country { get; set; } = string.Empty; //  Stát
        public string Region { get; set; } = string.Empty;  //  Kraj (oblast)
    }
}
