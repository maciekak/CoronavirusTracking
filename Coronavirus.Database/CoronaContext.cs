using Coronavirus.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coronavirus.Database
{
    public class CoronaContext : DbContext
    {

        public DbSet<Auth> Auths { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Cube> Cubes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-F40202H;Initial Catalog=CoronaTracking;Integrated Security=true;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1
            });
            //Property Configurations
            modelBuilder.Entity<Cube>()
                .HasKey(cube => new { cube.LatId, cube.LongId, cube.TimeId });
            modelBuilder.Entity<Location>()
                .HasKey(p => p.LocationId);
            modelBuilder.Entity<Location>()
                .HasOne<Cube>(s => s.Cube)
                .WithMany(g => g.Locations)
                .HasForeignKey(s => new { s.LatId, s.LongId, s.TimeId});
        }
    }
}
