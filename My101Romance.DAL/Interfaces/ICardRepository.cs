using My101Romance.Domain;
using My101Romance.Domain.Entity;

namespace My101Romance.DAL.Interfaces;

public interface ICardRepository : IBaseRepository<Card>
{
    Task<Card?> GetByTitle(string title);
    
    Task<List<Card>> SelectEightCards();

    Task<IEnumerable<Card?>> GetTop();

    Task<IEnumerable<Card>> GetTop18Plus();

    Task<Card> GetCardById(int cardId);

    Task UpdateCard(Card card);
}