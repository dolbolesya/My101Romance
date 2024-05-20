using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace My101Romance.Domain.Entity
{
    public class AppUser : IdentityUser
    {
        //[Range(0, 3)] // Limit the number of attempts to 3
        //public int AttemptsLeft { get; set; } = 0; // Set the default value to 0 attempts

        //[Range(0, 8)]
        //public int SelectedPairsCount  { get; set; } = 0;
    }
}

