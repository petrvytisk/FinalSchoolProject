namespace FinalSchoolProject.DTO {
    public class OrderDTO {
        public int Id { get; set; }
        public DateOnly Accepted { get; set; }
        public DateOnly Deadline { get; set; }
        public string Description { get; set; } = string.Empty;
        public double TotalPrice { get; set; }
        public int DaysLeft { get; set; }
    }
}
