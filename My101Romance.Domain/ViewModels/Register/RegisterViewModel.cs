using System.ComponentModel.DataAnnotations;

namespace My101Romance.Domain.ViewModels.Register
{
    public class RegisterViewModel
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }


        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username can only contain letters and digits.")]
        public string ErrorMessage { get; set; } = "";
    }
}