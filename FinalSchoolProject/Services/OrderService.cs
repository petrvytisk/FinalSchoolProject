
using FinalSchoolProject.DTO;

namespace FinalSchoolProject.Services {
    public class OrderService {
        private ApplicationDbContext dbContext;

        public OrderService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        internal IEnumerable<OrderDTO> GetAllOrders() {
            var allOrders = dbContext.Orders;
            var orderDTOs = new List<OrderDTO>();
            foreach (var order in allOrders) {
                orderDTOs.Add(new OrderDTO {
                    Id = order.Id,
                    Accepted = order.Accepted,
                    Deadline = order.Deadline,
                    Description = order.Description,
                    TotalPrice = order.TotalPrice,
                    DaysLeft = order.DaysLeft,
                });
            }
            return orderDTOs;
        }
    }
}
