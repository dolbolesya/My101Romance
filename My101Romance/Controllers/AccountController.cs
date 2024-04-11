using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using My101Romance.Domain.Entity;
using My101Romance.Domain.ViewModels.Login;
using My101Romance.Services.Interfaces;

namespace My101Romance.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAccountService _accountService;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAccountService accountService) 
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _accountService = accountService;
    }
    
    
    public IActionResult Login()
    {
        var response = new LoginViewModel();
        return View("auth/Login");
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("auth/Login",loginViewModel);
        }

        var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
        if (user != null)
        {   
            //User is found, check pwd
            var pwdCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (pwdCheck)
            {
                //pwd correct, sing in
                var result = await _signInManager
                    .PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            //pwd is incorrect
            TempData["Error"] = "Wrong. Please, try again!";
            return View("auth/Login",loginViewModel);
        }
        //user not found
        TempData["Error"] = "Wrong. User not found!";
        return View("auth/Login",loginViewModel);


    }


    public async Task<IActionResult> AddUser()
    {
        var response = await _accountService.AddUser();
        return View("dev/AddUser",response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(int id)
    {
        var response = await _accountService.GetUser(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View("dev/GetUser",response.Data);
        }
        return RedirectToAction("Error", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Register()
    {
        return View("auth/Register");
    }
}