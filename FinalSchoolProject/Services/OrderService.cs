using FinalSchoolProject.Models;
using FinalSchoolProject.DTO;
using Microsoft.EntityFrameworkCore;

namespace FinalSchoolProject.Services {
    public class OrderService {
        private ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }
        // Metoda načte všechny objednávky a vrátí je ve formě DTO
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync() {
            var allOrders = await _dbContext.Orders.ToListAsync();
            var orderDTOs = new List<OrderDTO>();
            foreach (var order in allOrders) {
                orderDTOs.Add(modelToDto(order));
            }
            return orderDTOs;
        }

        // Metoda uloží novou objednávku
        public async Task CreateAsync(OrderDTO newOrder) {
            await _dbContext.Orders.AddAsync(DtoToModel(newOrder));
            await _dbContext.SaveChangesAsync();
        }

        // Mapovací metoda MODEL => DTO
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

        // Mapovací metoda DTO => MODEL
        private Order DtoToModel(OrderDTO orderDto) {
            return new Order() {
                Accepted = orderDto.Accepted,
                Deadline = orderDto.Deadline,
                DaysLeft = orderDto.DaysLeft,
                Status = orderDto.Status,
                //Customer.Id = orderDto.CustomerId, // Id by se mělo vyplnit samo
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