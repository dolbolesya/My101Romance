using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using My101Romance.Domain.Entity;
using My101Romance.Domain.ViewModels.Login;
using My101Romance.Domain.ViewModels.Register;
using My101Romance.Services.Interfaces;

namespace My101Romance.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAccountService _accountService;
    
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, 
        IAccountService accountService, ILogger<AccountController> logger) 
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _accountService = accountService;
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        var response = new LoginViewModel();
        return View("auth/Login");
    }
    
    [HttpPost]
    public async Task<IActionResult> 
        Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("auth/Login", loginViewModel);
        }

        var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password. Please try again.");
                return View("auth/Login", loginViewModel);
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "User not found.");
            return View("auth/Login", loginViewModel);
        }
    }



    [Authorize(Roles = "root, admin")]
    public async Task<IActionResult> AddUser(RegisterViewModel model)
    {
        var response = await _accountService.AddUser(model);
        return View("dev/AddUser",response.Data);
    }

    
    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View("auth/Register");
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // logging
            _logger.LogInformation("Received registration request: {@Model}", model);

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                //user with this email
                ModelState.AddModelError(nameof(RegisterViewModel.Email), "User with this email already exists.");
                return View("auth/Register", model);
            }
            
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email
                
            };
            
            if (!string.IsNullOrEmpty(model.Password))
            {
                // set password
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            }
            else
            {
                // show err if pass empty
                ModelState.AddModelError(nameof(RegisterViewModel.Password), "Please enter a password.");
                return View("auth/Register", model);
            }


            var response = await _userManager.CreateAsync(user, model.Password);
            if (response.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in response.Errors)
                {
                    // log err
                    _logger.LogError("Error: {Description}", error.Description);

                    // log
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

        }
        else
        {
            // log
            _logger.LogWarning("Invalid model state during registration: {@Model}", model);
        
            // log err for username
            ModelState.AddModelError(nameof(RegisterViewModel.UserName), "Please enter a valid username.");
        
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogWarning("Validation error: {ErrorMessage}", error.ErrorMessage);
            }
        }

        // return view if all ok
        return View("auth/Register", model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("index", "Home");
    }
}