using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels
{
    public class LoginVM
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
