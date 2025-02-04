using FinalSchoolProject.Models;
using FinalSchoolProject.DTO;
using FinalSchoolProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;

namespace FinalSchoolProject.Services {
    public class OrderService {
        private ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }
        // Metoda načte všechny objednávky a vrátí je ve formě DTO
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync() {
            var allOrders = await _dbContext.Orders.Include(o=>o.Status).Include(o => o.Customer).ToListAsync();
            var orderDTOs = new List<OrderDTO>();
            foreach (var order in allOrders) {
                orderDTOs.Add(ModelToDto(order));
            }
            return orderDTOs;
        }

        public async Task CreateAsync(OrderDTO newOrder, decimal totalPriceDecimal) {
            Order orderToInsert = await DtoToModelAsync(newOrder, totalPriceDecimal);
            //if (orderToInsert.Customer != null) {   // tato kontrola už se selectem asi nemá význam
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
        // EDIT krok 3
        // Mapovací metoda MODEL => DTO
        private OrderDTO ModelToDto(Order order) {
            order.DaysLeft = (order.Deadline - DateTime.Now).Days;
            return new OrderDTO() {
                Id = order.Id,
                Accepted = order.Accepted,
                Deadline = order.Deadline,
                DaysLeft = order.DaysLeft,
                StatusId = order.StatusId,
                StatusName = order.Status?.Name ?? "Neznámý", // Získání názvu statusu
                CustomerId = order.CustomerId,
                CompanyName = order.Customer.CompanyName,   //nové
                Title = order.Title,
                Description = order.Description,
                InvoiceNum = order.InvoiceNum,
                PriceOfferNum = order.PriceOfferNum,
                DeliveryNoteNum = order.DeliveryNoteNum,
                TotalPrice = order.TotalPrice.ToString("N", new CultureInfo("cs-CZ")),
            };
        }

        // Mapovací metoda DTO => MODEL
        private async Task<Order> DtoToModelAsync(OrderDTO orderDto, decimal totalPriceDecimal) {
            return new Order() {
                Accepted = orderDto.Accepted,
                Deadline = orderDto.Deadline,
                DaysLeft = (orderDto.Deadline - DateTime.Now).Days,
                StatusId = orderDto.StatusId,
                CustomerId = orderDto.CustomerId,
                //Customer = _dbContext.Customers.FirstOrDefault(c => c.Id == orderDto.CustomerId),
                Title = orderDto.Title,
                Description = orderDto.Description,
                InvoiceNum = orderDto.InvoiceNum,
                PriceOfferNum = orderDto.PriceOfferNum,
                DeliveryNoteNum = orderDto.DeliveryNoteNum,
                TotalPrice = totalPriceDecimal,
            };
        }

        // EDIT krok 2
        // vytáhne z databáze konkrétní objednávku dle ID (přidá k ní i zákazníka)
        public async Task<OrderDTO> GetByIdAsync(int id) {
            var order = await _dbContext.Orders.Include(o=>o.Customer).FirstOrDefaultAsync(c => c.Id == id);
            return ModelToDto(order); // vrátí údaje ve formě ViewModelu
        }

        // EDIT krok 5
        // ID se ve výsledku ani nevyužije, dalo by se ale udělat ověření, zda IDčka souhlasí
        public async Task<OrderDTO> UpdateOrderAsync(int id, OrderDTO updatedOrder) {
            decimal totalPriceDecimal;
            decimal.TryParse(updatedOrder.TotalPrice, NumberStyles.Any, new CultureInfo("cs-CZ"), out totalPriceDecimal);
            Order? existingOrder = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            existingOrder.Customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == updatedOrder.CustomerId);
            existingOrder.Accepted = updatedOrder.Accepted;
            existingOrder.Deadline = updatedOrder.Deadline;
            existingOrder.StatusId = updatedOrder.StatusId;
            existingOrder.CustomerId = updatedOrder.CustomerId;
            existingOrder.Title = updatedOrder.Title;
            existingOrder.Description = updatedOrder.Description;
            existingOrder.InvoiceNum = updatedOrder.InvoiceNum;
            existingOrder.PriceOfferNum = updatedOrder.PriceOfferNum;
            existingOrder.DeliveryNoteNum = updatedOrder.DeliveryNoteNum;
            existingOrder.TotalPrice = totalPriceDecimal;
            
            _dbContext.Orders.Update(existingOrder);
            await _dbContext.SaveChangesAsync();    // tímto je objednávka aktualizována
            return updatedOrder;     // je nutné toto vracet??
        }

        public async Task DeleteAsync(int id) {
            var orderToDelete = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            _dbContext.Orders.Remove(orderToDelete);
            _dbContext.SaveChanges();
        }
    }
}