using My101Romance.DAL.Interfaces;
using My101Romance.Domain.Entity;
using My101Romance.Domain.Enum;
using My101Romance.Domain.Response;
using My101Romance.Domain.ViewModels.Register;
using My101Romance.Services.Interfaces;

namespace My101Romance.Services.Implementations;

public class AccountService : IAccountService
{
    public readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IBaseResponse<AppUser>> RegisterUser(RegisterViewModel model)
    {
        var baseResponse = new BaseResponse<AppUser>();
        var user = new AppUser
        {
            UserName = model.UserName,
            Email = model.Email
        };
        
        

        var success = await _accountRepository.CreateUserAsync(user, model.Password);
        if (!success)
        {
            baseResponse.ErrDescription = "Failed to register user.";
            baseResponse.StatusCode = StatusCode.InternalServerError;
            return baseResponse;
        }
        
        baseResponse.Data = user;
        baseResponse.StatusCode = StatusCode.Ok;
        return baseResponse;
    }
    

    public async Task<IBaseResponse<AppUser>> AddUser(RegisterViewModel model)
    {
        var baseResponse = new BaseResponse<AppUser>();
        try
        {
            var user = new AppUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                //Password = model.Password
            };

            await _accountRepository.Create(user);
            


            baseResponse.Data = user;
            baseResponse.StatusCode = StatusCode.Ok;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<AppUser>()
            {
                ErrDescription = $"[AddUser]: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}