using Microsoft.AspNetCore.Identity;

namespace EmployeeStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}
