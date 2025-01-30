using System.ComponentModel.DataAnnotations;

namespace FinalSchoolProject.Models {
    public class Customer {
        public int Id { get; set; }
        [Required(ErrorMessage = "Název společnosti je povinný.")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "E-mail je povinný.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(\+?[0-9]{3}[ ]?)?[0-9]{3}[ ]?[0-9]{3}[ ]?[0-9]{3}$", ErrorMessage = "Zadejte platné české telefonní číslo.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "IČO je povinné.")]
        public string CIN { get; set; } // IČO
        public string? TIN { get; set; } // DIČ
        public int AddressId { get; set; } // Cizí klíč     smazán ?
        public Address? Address { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public int NumberOfOrders { get; set; }
        public DateOnly RegistrationDate { get; set; }
    }
}
