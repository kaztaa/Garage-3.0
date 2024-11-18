using System.ComponentModel.DataAnnotations;

public class ParkingSpot
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string SpotNumber { get; set; } = string.Empty;

    [Required]
    public bool IsOccupied { get; set; }

    public int ParkingSpotSize {  get; set; }
    public int? VehicleId { get; set; } // Nullable for empty spots
    

}
