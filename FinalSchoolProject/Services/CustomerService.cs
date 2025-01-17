namespace FinalSchoolProject.Services {
    public class CustomerService {
        private ApplicationDbContext _dbContext;

        public CustomerService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
    }
}
