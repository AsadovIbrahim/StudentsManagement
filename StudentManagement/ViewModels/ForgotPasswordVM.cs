using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels
{
    public class ForgotPasswordVM
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
