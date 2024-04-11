using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace My101Romance.Domain.Entity
{
    public class AppUser : IdentityUser
    {
        [Required]
        public  string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public  string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }
    }
}