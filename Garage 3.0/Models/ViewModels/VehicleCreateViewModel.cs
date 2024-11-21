using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage_3._0.Models.ViewModels
{
    public class VehicleCreateViewModel
    {
        public Vehicle Vehicle { get; set; }  // For vehicle details
        public IEnumerable<SelectListItem> VehicleTypes { get; set; } = new List<SelectListItem>();  // To store the vehicle types for the dropdown
    }
}
