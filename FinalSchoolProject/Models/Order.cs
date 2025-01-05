using System.ComponentModel.DataAnnotations.Schema; //je tohle potřeba?, j to v magistrech

namespace FinalSchoolProject.Models {
    public class Order {
        public int Id { get; set; }
        public DateOnly Accepted { get; set; }
        public DateOnly Deadline { get; set; }
        public string Description { get; set; }
        public double TotalPrice { get; set; }
        public int DaysLeft { get; set; }

    }
}
