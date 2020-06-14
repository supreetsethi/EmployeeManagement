using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }

    }
}