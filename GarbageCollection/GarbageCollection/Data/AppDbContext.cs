using Microsoft.EntityFrameworkCore;
using GarbageCollection.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GarbageCollection.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<CitizenModel> Citizens { get; set; }
        public DbSet<CollectionModel> Collections { get; set; }
        public DbSet<BinModel> Bins { get; set; }
        public DbSet<BinCitizenModel> BinCitizens { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}