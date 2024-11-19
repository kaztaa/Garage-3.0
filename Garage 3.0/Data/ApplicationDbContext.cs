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

        // DbSets for tables
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ApplicationUser - One-to-Many with Vehicle
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Vehicles)
                .WithOne(v => v.ApplicationUser)
                .HasForeignKey(v => v.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade); // If a user is deleted, delete their vehicles too

            // Vehicle - Many-to-One with VehicleType
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict); // When a VehicleType is deleted, do not delete related Vehicles

            // VehicleType Seed Data
            modelBuilder.Entity<VehicleType>().HasData(
                new VehicleType { Id = 1, Name = "Car" },
                new VehicleType { Id = 2, Name = "Motorcycle" },
                new VehicleType { Id = 3, Name = "Truck" }
            );

            // ParkingSpot - One-to-One with Vehicle
            modelBuilder.Entity<ParkingSpot>()
                .HasOne(p => p.Vehicle)
                .WithOne(v => v.ParkingSpot)
                .HasForeignKey<ParkingSpot>(p => p.VehicleId)
                .OnDelete(DeleteBehavior.SetNull); // If a vehicle is deleted, set VehicleId to null in ParkingSpot

            // Additional configurations for ParkingSpot
            modelBuilder.Entity<ParkingSpot>()
                .Property(p => p.IsOccupied)
                .IsRequired(); // IsOccupied is required
        }
    }
}
