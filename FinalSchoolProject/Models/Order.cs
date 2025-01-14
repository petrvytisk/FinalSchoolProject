using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection; //je tohle potřeba?, je to v magistrech

namespace FinalSchoolProject.Models {
    public class Order {
        public int Id { get; set; }
        [Display(Name = "Přijato")]
        public DateOnly Accepted { get; set; }
        [Display(Name = "Termín")]
        public DateOnly Deadline { get; set; }
        [Display(Name = "Zbývá dnů")]
        public int DaysLeft { get; set; }
        public string Status { get; set; } = string.Empty;
        public Customer Customer { get; set; }
        [Display(Name = "Předmět")]
        public string Title { get; set; } = string.Empty;
        [Display(Name = "Podrobnosti")]
        public string Description { get; set; } = string.Empty;
        public string Invoice { get; set; } = string.Empty;
        public string PriceOffer { get; set; } = string.Empty;
        public string DeliveryNote { get; set; } = string.Empty;
        public double TotalPrice { get; set; }
    }
}