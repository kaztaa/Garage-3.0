using Garage_3._0.Models;
using System.ComponentModel.DataAnnotations;

public class ParkingSpot
{
    [Key]
    public string Id { get; set; }

    [Required]
    public string SpotNumber { get; set; } = string.Empty;

    [Required]
    public bool IsOccupied { get; set; }

    public int ParkingSpotSize {  get; set; }
    public string? VehicleId { get; set; } // Nullable for empty spots
    
    public Vehicle Vehicle { get; set; }
}
