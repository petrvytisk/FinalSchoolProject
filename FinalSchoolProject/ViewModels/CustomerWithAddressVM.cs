using System.ComponentModel.DataAnnotations;

namespace FinalSchoolProject.ViewModels {
    public class CustomerWithAddressVM {
        // Zákazník
        public int Id { get; set; }
        [Required(ErrorMessage = "Název společnosti je povinný.")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "E-mail je povinný.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefonní číslo je povinné.")]
        [RegularExpression(@"^(\+?[0-9]{3}[ ]?)?[0-9]{3}[ ]?[0-9]{3}[ ]?[0-9]{3}$", ErrorMessage = "Zadejte platné české telefonní číslo.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "IČO je povinné.")]
        public string CIN { get; set; } // IČO
        public string? TIN { get; set; } // DIČ
        public DateOnly RegistrationDate { get; set; }
        // Adresa
        public int AddressId { get; set; } // PŘIDÁNO
        public string? Street { get; set; }  //  ulice
        [Required(ErrorMessage = "Město je povinné.")]
        public string City { get; set; }   // město
        [Required(ErrorMessage = "PSČ je povinné.")]
        [RegularExpression(@"^\d{3} \d{2}$", ErrorMessage = "PSČ musí být ve formátu 123 45.")]
        public string PostalCode { get; set; }  // PSČ
        [Required(ErrorMessage = "Číslo popisné je povinné.")]
        public string HouseNumber { get; set; } // číslo popisné
        public string? StreetNumber { get; set; } //  číslo orientační
        [Required(ErrorMessage = "Stát je povinný.")]
        public string Country { get; set; } //  Stát
        public string? Region { get; set; }  //  Kraj (oblast)
    }
}
