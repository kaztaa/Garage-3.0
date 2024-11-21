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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ApplicationUser - One-to-Many with Vehicle
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(u => u.Vehicles)
            //    .WithOne()
            //    .HasForeignKey(v => v.ApplicationUserId)  // Corrected foreign key
            //    .OnDelete(DeleteBehavior.Cascade); // When the user is deleted, delete their vehicles
            modelBuilder.Entity<ApplicationUserVehicleClass>()
                .HasKey(auvc => auvc.VehicleId); // Primary Key on VehicleId

            modelBuilder.Entity<ApplicationUserVehicleClass>()
                .HasIndex(auvc => auvc.ApplicationUserId) // Unique Index
                .IsUnique();

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.ApplicationUserId)
                .IsRequired();

            // Vehicle - Many-to-One with VehicleType
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType);
                //.WithMany(t => t.Vehicles)  // Assuming you want this bidirectional
                //.HasForeignKey(v => v.VehicleTypeId)
                //.OnDelete(DeleteBehavior.Restrict); // Restrict deletion if still in use

            // Vehicle - One-to-One with ParkingSpot
            modelBuilder.Entity<ParkingSpot>()
                .HasOne(p => p.Vehicle)
                .WithOne(v => v.ParkingSpot)
                .HasForeignKey<ParkingSpot>(p => p.VehicleId)  // Foreign key in ParkingSpot
                .OnDelete(DeleteBehavior.SetNull); // Set VehicleId to null when Vehicle is deleted

            // Additional configuration for ParkingSpot
            modelBuilder.Entity<ParkingSpot>()
                .Property(p => p.IsOccupied)
                .IsRequired();  // Make IsOccupied required

            //modelBuilder.Entity<ApplicationUserVehicleClass>()
            //    .HasKey(t => new { t.ApplicationUserId, t.VehicleId });
            // Define composite key for the join table (ApplicationUserVehicleClass)
            //modelBuilder.Entity<ApplicationUserVehicleClass>()
            //    .HasKey(auvc => new { auvc.ApplicationUserId, auvc.VehicleId });

            //// Optionally, if needed, define unique indexes as well
            //modelBuilder.Entity<ApplicationUserVehicleClass>()
            //    .HasIndex(auvc => auvc.ApplicationUserId)
            //    .IsUnique();
        }

        //public DbSet<Vehicle> vehicles { get; set; }
    }
}