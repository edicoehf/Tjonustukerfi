using Microsoft.EntityFrameworkCore;
using ThjonustukerfiWebAPI.Models.Entities;

namespace ThjonustukerfiWebAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemOrderConnection> ItemOrderConnection { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<ServiceState> ServiceState { get; set; }
    }
}