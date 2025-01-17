using FinalSchoolProject.DTO;
using FinalSchoolProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalSchoolProject.Services {
    public class OrderService {
        private ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync() {
            var allOrders = await _dbContext.Orders.ToListAsync();
            var orderDTOs = new List<OrderDTO>();
            foreach (var order in allOrders) {
                orderDTOs.Add(modelToDto(order));
            }
            return orderDTOs;
        }

        public async Task CreateAsync(OrderDTO newOrder) {
            await _dbContext.Orders.AddAsync(DtoToModel(newOrder));
            await _dbContext.SaveChangesAsync();
        }

        private OrderDTO modelToDto(Order order) {
            return new OrderDTO() {
                Id = order.Id,
                Accepted = order.Accepted,
                Deadline = order.Deadline,
                DaysLeft = order.DaysLeft,
                Status = order.Status,
                CustomerId = order.Customer.Id,
                Title = order.Title,
                Description = order.Description,
                InvoiceNum = order.InvoiceNum,
                PriceOfferNum = order.PriceOfferNum,
                DeliveryNoteNum = order.DeliveryNoteNum,
                TotalPrice = order.TotalPrice,
            };
        }

        private Order DtoToModel(OrderDTO orderDto) {
            return new Order() {
                Id = orderDto.Id,
                Accepted = orderDto.Accepted,
                Deadline = orderDto.Deadline,
                DaysLeft = orderDto.DaysLeft,
                Status = orderDto.Status,
                //Customer.Id = orderDto.CustomerId, //asi zrušit
                Title = orderDto.Title,
                Description = orderDto.Description,
                InvoiceNum = orderDto.InvoiceNum,
                PriceOfferNum = orderDto.PriceOfferNum,
                DeliveryNoteNum = orderDto.DeliveryNoteNum,
                TotalPrice = orderDto.TotalPrice,
            };
        }
    }
}