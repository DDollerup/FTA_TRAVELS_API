using Microsoft.EntityFrameworkCore;

namespace FTAAPI.Models
{
    public class FTAContext : DbContext
    {
        public FTAContext(DbContextOptions<FTAContext> options) : base(options) { }

        public DbSet<About> Abouts => Set<About>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Destination> Destinations => Set<Destination>();
        public DbSet<DestinationImage> DestinationImages => Set<DestinationImage>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Subscribe> Subscribers => Set<Subscribe>();

    }
}
