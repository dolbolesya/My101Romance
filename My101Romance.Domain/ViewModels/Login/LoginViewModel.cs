using System.ComponentModel.DataAnnotations;

namespace My101Romance.Domain.ViewModels.Login;

public class LoginViewModel
{
    [Required(ErrorMessage = "Username is required!")]
    [Display(Name = "username")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Email address is required!")]
    [Display(Name = "email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required!")]
    [DataType(DataType.Password)]
    public string Pwd { get; set; }
    
    
}