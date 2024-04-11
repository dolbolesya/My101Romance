using My101Romance.Domain.Entity;
using My101Romance.Domain.Response;

namespace My101Romance.Services.Interfaces;

public interface IAccountService
{ 
    Task<IBaseResponse<AppUser>> AddUser();

    Task<IBaseResponse<AppUser>> GetUser(int id);
}