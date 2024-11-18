using Garage_3._0.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Garage_3._0.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets för tabellerna
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ApplicationUser - One-to-Many med Vehicle
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Vehicles)
                .WithOne()
                .HasForeignKey(v => v.Id)
                .OnDelete(DeleteBehavior.Cascade); // Om användare tas bort, ta bort deras fordon också

            // Vehicle - Many-to-One med VehicleType
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType);
                //.WithMany(t => t.Vehicles)
                //.HasForeignKey(v => v.VehicleTypeId)
                //.OnDelete(DeleteBehavior.Restrict); // Begränsa borttagning av VehicleType om fordon använder det

            // Vehicle - One-to-One med ParkingSpot
            modelBuilder.Entity<ParkingSpot>()
                .HasOne(p => p.Vehicle)
                .WithOne(v => v.ParkingSpot)
                .HasForeignKey<ParkingSpot>(p => p.VehicleId)
                .OnDelete(DeleteBehavior.SetNull); // Om ett fordon tas bort, sätt VehicleId till null

            // Övriga konfigurationer
            modelBuilder.Entity<ParkingSpot>()
                .Property(p => p.IsOccupied)
                .IsRequired(); // IsOccupied är obligatoriskt
        }
    }
}