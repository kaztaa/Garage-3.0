using Garage_3._0.Models;
using System.ComponentModel.DataAnnotations;

public class ParkingSpot
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string SpotNumber { get; set; } = string.Empty;

    [Required]
    public bool IsOccupied { get; set; }

    public int ParkingSpotSize { get; set; }

    // Foreign key for Vehicle (should match the primary key type of Vehicle)
    public int? VehicleId { get; set; } // Nullable for empty spots

    public virtual Vehicle Vehicle { get; set; }
}
