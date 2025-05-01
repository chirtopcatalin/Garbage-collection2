using Microsoft.EntityFrameworkCore;
using GarbageCollection.Models;

namespace GarbageCollection.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CitizenModel> Citizens { get; set; }
        public DbSet<CollectionModel> Collections { get; set; }
        public DbSet<BinModel> Bins { get; set; }
        public DbSet<BinCitizenModel> BinCitizens { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}