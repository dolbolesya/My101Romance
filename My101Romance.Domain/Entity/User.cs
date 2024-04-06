using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace My101Romance.Domain.Entity;

public class User : IdentityUser
{
    [Required]
    [Key]
    public uint Id { get; init; }
    
    public string? Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    
    public string? Pwd { get; set; }
}