using System.ComponentModel.DataAnnotations;

namespace FinalSchoolProject.DTO {
    public class OrderDTO {
        public int Id { get; set; }
        [Required(ErrorMessage = "Datum přijetí je povinné.")]
        public DateTime Accepted { get; set; }
        [Required(ErrorMessage = "Termín dokončení je povinný.")]
        public DateTime Deadline { get; set; }
        public int DaysLeft { get; set; }
        //[Required]
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Předmět (stručný popis) objednávky je povinný.")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? InvoiceNum { get; set; }
        public string? PriceOfferNum { get; set; }
        public string? DeliveryNoteNum { get; set; }
        [Required(ErrorMessage = "Celková cena je povinná.")]
        public decimal TotalPrice { get; set; }
    }
}
