namespace FinalSchoolProject.DTO {
    public class CustomerDTO {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CIN { get; set; } // IČO
        public string? TIN { get; set; } // DIČ
        public int? AddressId { get; set; }
        public ICollection<int> OrderIds { get; set; } = new List<int>();
        public int NumberOfOrders { get; set; }
        public DateOnly RegistrationDate { get; set; }
    }
}
