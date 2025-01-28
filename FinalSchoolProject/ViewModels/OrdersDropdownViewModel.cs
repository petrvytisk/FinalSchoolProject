using FinalSchoolProject.Models;

namespace FinalSchoolProject.ViewModels {
    public class OrdersDropdownViewModel {
        public List<Customer> Customers { get; set; }
        public List<Status> Statuses { get; set; }  //
        public OrdersDropdownViewModel() {
            Customers = new List<Customer>();
            Statuses = new List<Status>();  //
        }
    }
}
