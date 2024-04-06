using Microsoft.AspNetCore.Mvc;
using My101Romance.Domain.ViewModels.Login;

namespace My101Romance.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        
    }
}