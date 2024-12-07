using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "The Email field is not a valid email address.")]
        public string Email { get; set; }


        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and contain an uppercase letter, a lowercase letter, a number, and a special character.")]

        public string Password { get; set; }
        [Required]
        [MinLength(8)]

        [DataType(DataType.Password), Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }
    }
}
