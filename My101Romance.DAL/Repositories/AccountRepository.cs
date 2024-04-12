using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using My101Romance.DAL.Interfaces;
using My101Romance.Domain.Entity;

namespace My101Romance.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public AccountRepository(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<bool> Create(AppUser? entity)
        {
            try
            {
                _db.User.Add(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<AppUser?> Get(int id)
        {
            return await _db.User.FindAsync(id);
        }

        public async Task<List<AppUser?>> Select()
        {
            return await _db.User.ToListAsync();
        }

        public async Task<bool> Delete(AppUser? entity)
        {
            try
            {
                _db.User.Remove(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<AppUser?> Update(AppUser? entity)
        {
            try
            {
                _db.User.Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AppUser?> FindByEmailAsync(string modelEmail)
        {
            return await _db.User.FirstOrDefaultAsync(u => u.Email == modelEmail);
        }

        public async Task<bool> CreateUserAsync(AppUser user, string? password)
        {
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                return result.Succeeded;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}