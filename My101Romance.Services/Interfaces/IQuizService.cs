using My101Romance.Domain.Entity;

namespace My101Romance.Services.Interfaces;

public interface IQuizService
{
    Task<List<Card>> TakeCards();
    
}