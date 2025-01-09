
using FinalSchoolProject.DTO;
using FinalSchoolProject.Models;

namespace FinalSchoolProject.Services {
    public class OrderService {
        private ApplicationDbContext dbContext;

        public OrderService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync() {
            var allOrders = dbContext.Orders;
            var orderDTOs = new List<OrderDTO>();
            foreach (var order in allOrders) {
                orderDTOs.Add(modelToDto(order));
            }
            return orderDTOs;
        }

        private OrderDTO modelToDto(Order order) {
            return new OrderDTO() {
                Id = order.Id,
                Accepted = order.Accepted,
                Deadline = order.Deadline,
                DaysLeft = order.DaysLeft,
                Status = order.Status,
                Customer = order.Customer,
                Title = order.Title,
                Description = order.Description,
                Invoice = order.Invoice,
                PriceOffer = order.PriceOffer,
                DeliveryNote = order.DeliveryNote,
                TotalPrice = order.TotalPrice,
            };
        }
    }
}