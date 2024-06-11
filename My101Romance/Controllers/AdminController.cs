using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using My101Romance.Domain.Entity;
using My101Romance.Domain.ViewModels.Roles;

namespace My101Romance.Controllers;

//[Authorize(Roles = "root, admin")]
public class AdminController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public AdminController(RoleManager<IdentityRole> roleManager,
        UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }


    [HttpGet]
    public async Task<IActionResult> CreateRole()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Add logic to create the role based on the model data
            // For example, using the RoleManager from Identity

            var role = new IdentityRole(model.RoleName);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                // Role created successfully
                return RedirectToAction("CreateRole", "Admin");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        // If model state is not valid or role creation fails, return to the view with the model
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> RoleList()
    {
        var roles = _roleManager.Roles;
        return View(roles);
    }
    

    [HttpGet]
    public async Task<IActionResult> EditRole(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        var model = new EditRoleViewModel
        {
            Id = role.Id,
            RoleName = role.Name
            // Add any other properties you want to edit
        };

        foreach (var user in _userManager.Users)
        {
            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                model.Users.Add(user.UserName);
            }
        }

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> EditRole(EditRoleViewModel model)
    {
        if (model == null)
        {
            return NotFound();
        }

        var role = await _roleManager.FindByIdAsync(model.Id);
        if (role == null)
        {
            return NotFound();
        }
        else
        {
            role.Name = model.RoleName;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("RoleList", "Admin");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            
            return View(model);
        }

        
    }
    

    [HttpGet]
    public async Task<IActionResult> EditUsersInRole(string roleId)
    {
        ViewBag.roleId = roleId;

        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            ViewBag.ErrorMessage = $"Role with Id = {roleId} not found";
            return NotFound();
        }

        var model = new List<UserRoleViewModel>();

        foreach (var user in _userManager.Users)
        {
            var userRoleViewModel = new UserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
            };
            
            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                userRoleViewModel.isSelected = true;
            }
            else
            {
                userRoleViewModel.isSelected = false;
            }
            
            model.Add(userRoleViewModel);
        }

        return View(model);

    }

 
    [HttpPost]
    public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        if (role == null)
        {
            ViewBag.ErrorMessage = $"Role with Id = {roleId} not found";
            return NotFound();
        }

        for (int i = 0; i < model.Count; i++)
        {
            var user = await _userManager.FindByIdAsync(model[i].UserId);

            IdentityResult result = null;

            if (model[i].isSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
            {
                result = await _userManager.AddToRoleAsync(user, role.Name);
            }
            else if (!model[i].isSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
            {
                result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            }
            else
            {
                continue;
            }

            if (result.Succeeded)
            {
                if (i < (model.Count - 1))
                {
                    continue;
                }
                else
                {
                    return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
        }
        return View();
    }
    

}