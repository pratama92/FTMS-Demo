using FTMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTMS.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Booking> Booking => Set<Booking>();
        public DbSet<User> User => Set<User>();
        public DbSet<Trip> Trips => Set<Trip>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}