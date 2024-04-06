using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace My101Romance.Domain.Entity;

public class AppUser : IdentityUser
{
    [Required]
    [Key]
    public uint Id { get; init; }
    
    public string? Username { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string? Pwd { get; set; }
}