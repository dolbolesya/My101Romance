using System.ComponentModel.DataAnnotations;

namespace My101Romance.Domain.ViewModels.Roles
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Role name is required")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}