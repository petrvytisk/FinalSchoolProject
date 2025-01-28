using System.ComponentModel.DataAnnotations;

namespace FinalSchoolProject.Models {
    public class Address {
        public int Id { get; set; }
        public string? Street { get; set; }  //  ulice
        [Required(ErrorMessage = "Město je povinné.")]
        public string City { get; set; }    // město
        [Required(ErrorMessage = "PSČ je povinné.")]
        [RegularExpression(@"^\d{3} \d{2}$", ErrorMessage = "PSČ musí být ve formátu 123 45.")]
        public string PostalCode { get; set; }  // PSČ
        [Required(ErrorMessage = "Číslo popisné je povinné.")]
        public string HouseNumber { get; set; } // číslo popisné
        public string? StreetNumber { get; set; } //  číslo orientační
        [Required(ErrorMessage = "Stát je povinný.")]
        public string Country { get; set; } //  stát
        public string? Region { get; set; }  //  kraj (oblast)
        // Navigační vlastnost na Customer
        public Customer? Customer { get; set; }
        // Nepovinný cizí klíč (volitelné, ale doporučené pro explicitnost) - lze pak jednosuše zahrnout pomocí ".Include"
        public int? CustomerId { get; set; }
    }
}