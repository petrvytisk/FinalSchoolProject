using FinalSchoolProject.Models;
using Microsoft.EntityFrameworkCore;

internal class ApplicationDbContext :DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

    }
    public DbSet<Order> Orders { get; set; }
}