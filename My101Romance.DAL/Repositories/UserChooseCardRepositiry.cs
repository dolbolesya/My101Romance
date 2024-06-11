using My101Romance.Domain.Entity;

namespace My101Romance.DAL.Repositories;

public class UserChooseCardRepositiry
{
    private readonly AppDbContext _db;

    public UserChooseCardRepositiry(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<bool> Create(Card entity)
    {
        _db.UserChooseCard!.Add(entity);
        await _db.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> Create2(Card entity)
    {
        _db.UserChooseCard!.Add(entity);
        await _db.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> Create3(Card entity)
    {
        _db.UserChooseCard!.Add(entity);
        await _db.SaveChangesAsync();
        
        return true;
    }
}