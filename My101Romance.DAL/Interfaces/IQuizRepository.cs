using My101Romance.Domain.Entity;

namespace My101Romance.DAL.Interfaces;

public interface IQuizRepository
{
    Task<List<Card>> TakeCards();
}