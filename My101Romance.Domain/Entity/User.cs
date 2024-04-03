using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My101Romance.Domain.Entity;

public class User
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