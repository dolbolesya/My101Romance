using My101Romance.Domain.Entity;
using My101Romance.Domain.Response;

namespace My101Romance.DAL.Interfaces;

public interface IAccountRepository : IBaseRepository<AppUser>
{
    Task<AppUser?> FindByEmailAsync(string modelEmail);

    Task<bool> CreateUserAsync(AppUser user, string? password);
}