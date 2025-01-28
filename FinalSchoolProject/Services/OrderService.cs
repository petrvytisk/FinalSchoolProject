using FinalSchoolProject.Models;
using FinalSchoolProject.DTO;
using FinalSchoolProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalSchoolProject.Services {
    public class OrderService {
        private ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }
        // Metoda načte všechny objednávky a vrátí je ve formě DTO
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync() {
            var allOrders = await _dbContext.Orders.Include(o=>o.Status).ToListAsync();  //INCLUDE???
            var orderDTOs = new List<OrderDTO>();
            foreach (var order in allOrders) {
                orderDTOs.Add(modelToDto(order));
            }
            return orderDTOs;
        }

        // Metoda uloží novou objednávku    PŮVODNÍ MOJE
        //public async Task CreateAsync(OrderDTO newOrder) {
        //    await _dbContext.Orders.AddAsync(DtoToModel(newOrder));
        //    await _dbContext.SaveChangesAsync();
        //}

        public async Task CreateAsync(OrderDTO newOrder) {
            Order orderToInsert = await DtoToModelAsync(newOrder);
            //if (orderToInsert.Customer != null) {
                await _dbContext.Orders.AddAsync(orderToInsert);
                await _dbContext.SaveChangesAsync();
            //}
        }

        // Metoda načte data o zákaznících pro SELECT
        public async Task<OrdersDropdownViewModel> GetNewOrdersDropdownValues() {
            var ordersDropdownData = new OrdersDropdownViewModel() {
                Customers = await _dbContext.Customers.OrderBy(c =>
                c.CompanyName).ToListAsync(),
                Statuses = await _dbContext.Statuses.OrderBy(s =>
                s.Id).ToListAsync(),
            };
            return ordersDropdownData;
        }

        // Mapovací metoda MODEL => DTO
        private OrderDTO modelToDto(Order order) {
            order.DaysLeft = (order.Deadline - DateTime.Now).Days;
            return new OrderDTO() {
                Id = order.Id,
                Accepted = order.Accepted,
                Deadline = order.Deadline,
                DaysLeft = order.DaysLeft,
                StatusName = order.Status?.Name ?? "Neznámý", // Získání názvu statusu
                CustomerId = order.CustomerId,
                Title = order.Title,
                Description = order.Description,
                InvoiceNum = order.InvoiceNum,
                PriceOfferNum = order.PriceOfferNum,
                DeliveryNoteNum = order.DeliveryNoteNum,
                TotalPrice = order.TotalPrice,
            };
        }

        // Mapovací metoda DTO => MODEL
        private async Task<Order> DtoToModelAsync(OrderDTO orderDto) {
            orderDto.DaysLeft = (orderDto.Deadline - DateTime.Now).Days;
            return new Order() {
                Accepted = orderDto.Accepted,
                Deadline = orderDto.Deadline,
                DaysLeft = orderDto.DaysLeft,
                StatusId = orderDto.StatusId,
                CustomerId = orderDto.CustomerId,
                //Customer = _dbContext.Customers.FirstOrDefault(c => c.Id == orderDto.CustomerId),
                //Customer = _dbContext.Customers.Where(c => c.Id == orderDto.CustomerId).FirstOrDefault(), jiná možnost???
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