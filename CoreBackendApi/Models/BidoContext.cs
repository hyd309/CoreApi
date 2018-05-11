using Microsoft.EntityFrameworkCore;
namespace CoreBackendApi.Models
{
    public class BidoContext : DbContext
    {
        public BidoContext(DbContextOptions<BidoContext> options) : base(options)
        {
        }

        public DbSet<DeviceLocation> Locations { get; set; }
    }
}
