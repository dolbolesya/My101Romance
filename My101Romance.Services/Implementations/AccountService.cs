using My101Romance.DAL.Interfaces;
using My101Romance.Domain.Entity;
using My101Romance.Domain.Enum;
using My101Romance.Domain.Response;
using My101Romance.Services.Interfaces;

namespace My101Romance.Services.Implementations;

public class AccountService : IAccountService
{
    public readonly IAccountRepository _AccountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _AccountRepository = accountRepository;
    }

    public async Task<IBaseResponse<AppUser>> AddRoot()
    {
        var baseResponse = new BaseResponse<AppUser>();
        var user = new AppUser()
        {
            Username = "dolbolesya",
            Email = "root@mail.com",
            Pwd = "pass"
        };

        await _AccountRepository.Create(user);

        baseResponse.Data = user;
        baseResponse.StatusCode = StatusCode.Ok;
        return baseResponse;

    }
    
}