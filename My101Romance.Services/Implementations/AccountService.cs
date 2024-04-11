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

    public async Task<IBaseResponse<AppUser>> GetUser(int id)
    {
        var baseResponse = new BaseResponse<AppUser>();
        try
        {
            var user = await _AccountRepository.Get(id);
            if (user == null)
            {
                baseResponse.ErrDescription = $"Item not found by {id}";
                baseResponse.StatusCode = StatusCode.NotFound;
                return baseResponse;
            }

            baseResponse.Data = user;
            baseResponse.StatusCode = StatusCode.Ok;
            return baseResponse;

        }
        catch (Exception e)
        {
            return new BaseResponse<AppUser>()
            {
                ErrDescription = $"[GetUser]: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    
    public async Task<IBaseResponse<IEnumerable<AppUser>>> GetUsers()
    {
        var baseResponse = new BaseResponse<IEnumerable<AppUser>>();
        try
        {
            var users = await _AccountRepository.Select();
            if (users.Count == 0)
            {
                
                baseResponse.ErrDescription = "Found 0 elements";
                baseResponse.StatusCode = StatusCode.NotFound;
            }

            baseResponse.Data = users!;
            baseResponse.StatusCode = StatusCode.Ok;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<AppUser>>()
            {
                ErrDescription = $"[GetUsers]: {e.Message}",
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
                Pwd = "1111"
            };

            await _AccountRepository.Create(user);
            


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