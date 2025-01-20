using FinalSchoolProject.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext :DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Nastavení one-to-one vztahu
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Address) // Customer má jednu Address
            .WithOne(a => a.Customer) // Address má jednoho Customer
            .HasForeignKey<Address>(a => a.CustomerId); // Cizí klíč je v Address

        base.OnModelCreating(modelBuilder);
    }
}