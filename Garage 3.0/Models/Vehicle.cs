namespace Garage_3._0.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ParkingSpotSize { get; set; }
        public int NumberOfWheels { get; set; }

        // Foreign key for ApplicationUser
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        // Foreign key for VehicleType
        public int VehicleTypeId { get; set; }
        public virtual VehicleType VehicleType { get; set; }

        // Navigation property for ParkingSpot (One-to-One)
        public virtual ParkingSpot ParkingSpot { get; set; }
    }
}



//namespace Garage_3._0.Models
//{
//    public class Vehicle
//    {
//        public string? Id { get; set; }
//        public string RegistrationNumber { get; set; } = string.Empty;
//        public string Color { get; set; } = string.Empty;
//        public string Brand { get; set; } = string.Empty;
//        public string Model { get; set; } = string.Empty;
//        public int ParkingSpotSize { get; set; }
//        public int NumberOfWheels { get; set; }
//        //public virtual VehicleType? VehicleType { get; set; }

//        public ParkingSpot? ParkingSpot { get; set; }


//        // Foreign key till VehicleType
//        public int VehicleTypeId { get; set; }  // En foreign key som kopplar till VehicleType

//        // Navigeringsegenskap som refererar till VehicleType
//        public virtual VehicleType VehicleType { get; set; }  // Ett fordon har en VehicleType

//    }
//}
