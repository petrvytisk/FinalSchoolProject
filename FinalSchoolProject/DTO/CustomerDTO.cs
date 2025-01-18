namespace FinalSchoolProject.DTO {
    public class CustomerDTO {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CIN { get; set; } = string.Empty; // IČO
        public string TIN { get; set; } = string.Empty; // DIČ
        public ICollection<int> AddressIds { get; set; } = new List<int>();
        public ICollection<int> OrderIds { get; set; } = new List<int>();
        public int NumberOfOrders { get; set; }
        public DateOnly RegistrationDate { get; set; }
    }
}
