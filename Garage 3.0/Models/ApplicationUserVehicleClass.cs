using System.ComponentModel.DataAnnotations;

namespace Garage_3._0.Models
{
    public class ApplicationUserVehicleClass
    {
        // Primary key
        public string ApplicationUserId { get; set; }
        public int VehicleId { get; set; }

        // Foregin key
        public ApplicationUser ApplicationUser { get; set; }
        public Vehicle Vehicle { get; set; } // avkommentera när vi har Vehicle
    }
}
