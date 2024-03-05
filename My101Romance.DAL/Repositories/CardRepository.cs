using Microsoft.EntityFrameworkCore;
using My101Romance.DAL.Interfaces;
using My101Romance.Domain.Entity;

namespace My101Romance.DAL.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _db;

    public CardRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<bool> Create(Card? entity)
    {
        _db.Card!.Add(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<Card?> Get(int id)
    {
        return await _db.Card!.FirstOrDefaultAsync(x => x!.Id == id);
    }

    public async Task<List<Card>> Select()
    {
        return await _db.Card.ToListAsync();
    }

    public async Task<bool> Delete(Card? entity)
    { 
        _db.Card!.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<Card?> GetByTitle(string title)
    {
        return await _db.Card.FirstOrDefaultAsync(x => x!.Title == title);
    }
}