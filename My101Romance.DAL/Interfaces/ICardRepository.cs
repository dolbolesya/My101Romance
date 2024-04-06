using My101Romance.Domain;
using My101Romance.Domain.Entity;

namespace My101Romance.DAL.Interfaces;

public interface ICardRepository : IBaseRepository<Card>
{
    Task<Card?> GetByTitle(string title);
    
    Task<IEnumerable<Card>> SelectTwoCards();

    Task<IEnumerable<Card?>> GetTop();

    Task<IEnumerable<Card>> GetTop18Plus();
}