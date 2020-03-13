using Microsoft.EntityFrameworkCore;
using ThjonustukerfiWebAPI.Models.Entities;

namespace ThjonustukerfiWebAPI.Models
{
    public interface IDataContext
    {
        DbSet<Customer> Customer { get; set; }
        DbSet<Item> Item { get; set; }
        DbSet<ItemOrderConnection> ItemOrderConnection { get; set; }
        DbSet<Order> Order { get; set; }
        DbSet<Service> Service { get; set; }
        DbSet<State> State { get; set; }
        DbSet<ServiceState> ServiceState { get; set; }

        //* Error logs
        DbSet<Log> ExceptionLog { get; set; }
    }
}