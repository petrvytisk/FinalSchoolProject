
using System.ComponentModel.DataAnnotations;

namespace FinalSchoolProject.Models {
    public class Order {
        public int Id { get; set; }
        [Required]
        public DateTime Accepted { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        public int DaysLeft { get; set; }
        [Required]
        public int StatusId { get; set; } // Cizí klíč
        public Status Status { get; set; }  // Navigační vlastnost
        [Required]
        public int CustomerId { get; set; } // Cizí klíč
        [Required]
        public Customer Customer { get; set; }  // Navigační vlastnost
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? InvoiceNum { get; set; }
        public string? PriceOfferNum { get; set; }
        public string? DeliveryNoteNum { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
    }
}