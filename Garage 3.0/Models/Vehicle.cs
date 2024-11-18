namespace Garage_3._0.Models
{
    public class Vehicle
    {
        public string Id { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int ParkingSpotSize { get; set; }
        public int NumberOfWheels { get; set; }
        public int VehicleTypeId { get; set; }
        public virtual VehicleType? VehicleType { get; set; }

        public ParkingSpot ParkingSpot { get; set; }
    }
}
