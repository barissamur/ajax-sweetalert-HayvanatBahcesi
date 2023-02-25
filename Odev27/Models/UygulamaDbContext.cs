using Microsoft.EntityFrameworkCore;

namespace Odev27.Models
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {

        }

        public DbSet<Hayvan> Hayvanlar => Set<Hayvan>();
    }
}
