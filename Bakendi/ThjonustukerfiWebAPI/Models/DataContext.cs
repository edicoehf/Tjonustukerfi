using Microsoft.EntityFrameworkCore;
using ThjonustukerfiWebAPI.Models.Entities;

namespace ThjonustukerfiWebAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DataContext() {} // used for tests
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemOrderConnection> ItemOrderConnection { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<ServiceState> ServiceState { get; set; }

        //* Error logs
        public virtual DbSet<Log> ExceptionLog { get; set; }
    }
}