using Microsoft.EntityFrameworkCore;
using ThjonustukerfiWebAPI.Models.Entities;

namespace ThjonustukerfiWebAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    }
}