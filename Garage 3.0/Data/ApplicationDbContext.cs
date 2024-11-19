using Garage_3._0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Garage_3._0.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
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

            //// Vehicle - Many-to-One med VehicleType
            //modelBuilder.Entity<Vehicle>()
            //    .HasOne(v => v.VehicleType)
            //.WithMany(t => t.Vehicles)
            //.HasForeignKey(v => v.VehicleTypeId);
            ////.OnDelete(DeleteBehavior.Restrict); // Begränsa borttagning av VehicleType om fordon använder det

            // Konfiguration av en-till-många-relationen mellan Vehicle och VehicleType
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)  // Ett Vehicle har en VehicleType
                .WithMany(vt => vt.Vehicles)  // En VehicleType kan ha många Vehicles
                .HasForeignKey(v => v.VehicleTypeId)  // Set the foreign key
                .OnDelete(DeleteBehavior.Restrict);  // När en VehicleType tas bort, tas inte de kopplade fordonen bort

            // Seed data för VehicleType
            modelBuilder.Entity<VehicleType>().HasData(
                new VehicleType { Id = 1, Name = "Car" },
                new VehicleType { Id = 2, Name = "Motorcycle" },
                new VehicleType { Id = 3, Name = "Truck" }
            );


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