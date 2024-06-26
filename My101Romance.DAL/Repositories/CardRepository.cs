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
    
    public async Task<bool> Create(Card entity)
    {
        _db.Card!.Add(entity);
        await _db.SaveChangesAsync();
        
        return true;
    }

    public async Task<Card?> Get(int id)
    {
        return await _db.Card!.FirstOrDefaultAsync(x => x!.Id == id);
    }

    public async Task<List<Card?>> Select()
    {
        return await _db.Card.ToListAsync();
    }

    public async Task<bool> Delete(Card? entity)
    { 
        _db.Card!.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<Card> Update(Card entity)
    {
        _db.Card.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<List<Card>> SelectEightCards()
    {
        var randomCards = await _db.Card.OrderBy(x => Guid.NewGuid()).Take(2).ToListAsync();
        return randomCards;
    }
    
    public async Task<List<Card>> TakeCards(int count)
    {
        var randomCards = await _db.Card.OrderBy(x => Guid.NewGuid()).Take(count).ToListAsync();
        return randomCards;
    }

    public async Task<IEnumerable<Card?>> GetTop()
    {
        var cards = _db.Card
            .Where(c => c!.IsForAll == true)
            .OrderByDescending(c => c!.Rating)
            .ToList();

        return cards;

    }

    public async Task<IEnumerable<Card>> GetTop18Plus()
    {
        
        var cards = _db.Card
            .OrderByDescending(c => c!.Rating)
            .ToList();

        return cards;
    }

    public async Task<Card> GetCardById(int cardId)
    {
        return (await _db.Card.FindAsync(cardId))!;
    }

    public async Task UpdateCard(Card card)
    {
        _db.Entry(card).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }


    public async Task<Card?> GetByTitle(string title)
    {
        return await _db.Card.FirstOrDefaultAsync(x => x!.Title == title);
    }
    
}