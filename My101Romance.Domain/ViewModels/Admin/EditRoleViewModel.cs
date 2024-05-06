using System.ComponentModel.DataAnnotations;

namespace My101Romance.Domain.ViewModels.Roles;

public class EditRoleViewModel
{
    public EditRoleViewModel()
    {
        Users = new List<string>();
    }
    public string Id { get; set; }
    
    [Required(ErrorMessage = "Role name is required")]
    public string RoleName { get; set; }
    
    public List<string> Users { get; set; }
}