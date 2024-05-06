using Microsoft.EntityFrameworkCore;
using My101Romance.DAL.Interfaces;
using My101Romance.Domain.Entity;

namespace My101Romance.DAL.Repositories;

public class QuizRepositoty : IQuizRepository
{
    private readonly AppDbContext _db;
    
    public async Task<List<Card>> TakeCards()
    {
        var randomCards = await _db.Card.OrderBy(x => Guid.NewGuid()).Take(8).ToListAsync();
        return randomCards;
    }
}