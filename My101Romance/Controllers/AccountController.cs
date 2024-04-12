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
            // Логируем данные модели
            _logger.LogInformation("Received registration request: {@Model}", model);

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email
                
            };
            
            if (!string.IsNullOrEmpty(model.Password))
            {
                // Устанавливаем пароль для пользователя
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            }
            else
            {
                // Выводим сообщение об ошибке, если пароль не указан
                ModelState.AddModelError(nameof(RegisterViewModel.Password), "Please enter a password.");
                return View("auth/Register", model);
            }


            var response = await _userManager.CreateAsync(user, model.Password);
            if (response.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in response.Errors)
                {
                    // Выводим ошибки в логи
                    _logger.LogError("Error: {Description}", error.Description);

                    // Добавляем ошибки в ModelState для отображения на странице
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

        }
        else
        {
            // Логируем невалидное состояние модели и ошибки валидации
            _logger.LogWarning("Invalid model state during registration: {@Model}", model);
        
            // Добавляем ошибку в ModelState для поля UserName
            ModelState.AddModelError(nameof(RegisterViewModel.UserName), "Please enter a valid username.");
        
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogWarning("Validation error: {ErrorMessage}", error.ErrorMessage);
            }
        }

        // Возвращаем представление с моделью, содержащей ошибки валидации
        return View("auth/Register", model);
    }


    public IActionResult Logout()
    {
        throw new NotImplementedException();
    }
}