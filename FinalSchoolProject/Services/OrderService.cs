namespace FinalSchoolProject.Services {
    internal class OrderService {
        private ApplicationDbContext dbContext;

        public OrderService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
    }
}
