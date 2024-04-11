using System.ComponentModel.DataAnnotations;

namespace My101Romance.Domain.ViewModels.Register;

public class RegisterViewModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "Different passwords")]
    public string ConfirmPassword { get; set; }
}