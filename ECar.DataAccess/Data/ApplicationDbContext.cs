using ECar.Models;
using Microsoft.EntityFrameworkCore;

namespace ECar.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Gas> Gas { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Transimision> Transimisions { get; set; }
    }
}
