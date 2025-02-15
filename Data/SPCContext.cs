using Microsoft.EntityFrameworkCore;
using SPCManagementSystemAPI.Models;
using System.Reflection;

namespace SPCManagementSystemAPI.Data
{
    public class SPCContext : DbContext
    {
        public SPCContext(DbContextOptions<SPCContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
