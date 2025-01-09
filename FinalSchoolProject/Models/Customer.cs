namespace FinalSchoolProject.Models {
    public class Customer {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CIN { get; set; } = string.Empty; // IČO
        public string TIN { get; set; } = string.Empty; // DIČ
        public Address Address { get; set; }
        public int NumberOfOrders { get; set; }
        public DateOnly RegistrationDate { get; set; }



    }
}
