using FinalSchoolProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<AppUser> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
    }
    public DbSet<Order> Orders { get; set; }    //tabulka objednávek
    public DbSet<Customer> Customers { get; set; }  //tabulka zákazníků
    public DbSet<Address> Addresses { get; set; }   //tabulka adres
    public DbSet<Status> Statuses { get; set; } //tabulka fází projektu

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Nastavení one-to-one vztahu
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Address) // Customer má jednu Address
            .WithOne(a => a.Customer) // Address má jednoho Customer
            .HasForeignKey<Address>(a => a.CustomerId); // Cizí klíč je v Address

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Status>().HasData(
       new Status { Id = 1, Name = "PŘIJATO" },
       new Status { Id = 2, Name = "PŘÍPRAVA" },
       new Status { Id = 3, Name = "VÝROBA" },
       new Status { Id = 4, Name = "KONTROLA" },
       new Status { Id = 5, Name = "EXPEDICE" },
       new Status { Id = 6, Name = "FAKTURACE" },
       new Status { Id = 7, Name = "HOTOVO" },
       new Status { Id = 8, Name = "REKLAMACE" }
        );
    }
}