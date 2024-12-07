using Microsoft.AspNetCore.Identity;

namespace StudentManagement.Models.Concretes
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
