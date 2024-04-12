using My101Romance.Domain.Entity;
using My101Romance.Domain.Response;
using My101Romance.Domain.ViewModels.Register;

namespace My101Romance.Services.Interfaces;

public interface IAccountService
{ 
    Task<IBaseResponse<AppUser>> RegisterUser(RegisterViewModel model);
    Task<IBaseResponse<AppUser>> AddUser(RegisterViewModel model);

    
}