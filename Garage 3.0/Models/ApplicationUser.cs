using Microsoft.AspNetCore.Identity;

namespace Garage_3._0.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Vehicle> Vehicles { get; set; } //avkommentera när vi har Vehicle klass (1 användare kan ha många vehicles)
    }
}
