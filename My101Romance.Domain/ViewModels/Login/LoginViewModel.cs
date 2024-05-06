using System.ComponentModel.DataAnnotations;

namespace My101Romance.Domain.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email address is required!")]
        [Display(Name = "email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}