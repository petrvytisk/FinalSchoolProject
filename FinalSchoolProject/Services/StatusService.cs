using FinalSchoolProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalSchoolProject.Services {
    public class StatusService {
        private ApplicationDbContext _dbContext;

        public StatusService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        // Metoda načte data o statusech pro SELECT
        public async Task<OrdersDropdownViewModel> GetNewOrdersDropdownValues() {
            var ordersDropdownData = new OrdersDropdownViewModel() {
                Statuses = await _dbContext.Statuses.OrderBy(n =>
                n.Id).ToListAsync(),
            };
            return ordersDropdownData;
        }
    }
}
