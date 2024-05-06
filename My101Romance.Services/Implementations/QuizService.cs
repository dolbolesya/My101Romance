using My101Romance.DAL.Interfaces;
using My101Romance.Domain.Entity;
using My101Romance.Services.Interfaces;

namespace My101Romance.Services.Implementations;

public class QuizService : IQuizService
{
    public readonly IQuizService _QuizService;



    
    public async Task<List<Card>> TakeCards()
    {
        return await _QuizService.TakeCards();
    }

}