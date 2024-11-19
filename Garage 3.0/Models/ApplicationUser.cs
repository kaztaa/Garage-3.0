﻿using Microsoft.AspNetCore.Identity;

namespace Garage_3._0.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        // Collection of vehicles owned by this user
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
