
namespace FinalSchoolProject.Models {
    public class Order {
        public int Id { get; set; }
        public DateOnly Accepted { get; set; }
        public DateOnly Deadline { get; set; }
        public int DaysLeft { get; set; }
        public string Status { get; set; } = string.Empty;
        public Customer Customer { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string InvoiceNum { get; set; } = string.Empty;
        public string PriceOfferNum { get; set; } = string.Empty;
        public string DeliveryNoteNum { get; set; } = string.Empty;
        public double TotalPrice { get; set; }
    }
}