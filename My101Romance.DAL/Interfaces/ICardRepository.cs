using My101Romance.Domain;
using My101Romance.Domain.Entity;

namespace My101Romance.DAL.Interfaces;

public interface ICardRepository : IBaseRepository<Card>
{
    Task<Card> GetByTitle(string title);
}