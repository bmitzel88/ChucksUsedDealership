using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChucksUsedDealership.Models;

namespace ChucksUsedDealership.Data
{
    public class DealershipDbContext : IdentityDbContext
    {
        public DealershipDbContext(DbContextOptions<DealershipDbContext> options)
            : base(options)
        {
        }

        public DbSet<CarInventory> CarInventories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerCar> CustomerCars { get; set; }

        public DbSet<ServiceAppoinment> ServiceAppoinments { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<SalesPerson> SalesPeople { get; set; }

        public DbSet<ContactForm> ContactForms { get; set; }
    }
}
