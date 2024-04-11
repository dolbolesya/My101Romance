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

        try
        {
            var existingUser = await _accountRepository.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                baseResponse.ErrDescription = "User with this email already exists.";
                baseResponse.StatusCode = StatusCode.NotFound;
                return baseResponse;
            }

            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.UserName, // Assuming UserName is part of the RegisterViewModel
                Password = model.Password // Assuming Password is part of the RegisterViewModel
            };

            await _accountRepository.Create(user);

            baseResponse.Data = user;
            baseResponse.StatusCode = StatusCode.Ok;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<AppUser>
            {
                ErrDescription = $"Error registering user: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }    
    public async Task<IBaseResponse<AppUser>> AddUser()
    {
        var baseResponse = new BaseResponse<AppUser>();
        try
        {
            var user = new AppUser()
            {
                Email = "test@mail",
                UserName = "User",
                Password = "1111"
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