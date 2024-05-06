using Microsoft.AspNetCore.Mvc;
using My101Romance.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace My101Romance.Controllers
{
    [Authorize(Roles = "root,admin,user")]
    public class QuizController : Controller
    {
        private readonly IQuizService _quizService;
        private readonly ICardService _cardService;

        public QuizController(IQuizService quizService, ICardService cardService)
        {
            _quizService = quizService;
            _cardService = cardService;
        }
    
        [Authorize(Roles = "root,admin,user")]
        public async Task<IActionResult> Play()
        {
            var cards = await _cardService.GetEightCards();
            return View("game/Quiz", cards);
        }


        [HttpPost("/Quiz/SelectCard")] // Указываем явно URL для метода SelectCard
        public async Task<IActionResult> SelectCard([FromBody] int cardId)
        {
            var card = await _cardService.GetCardById(cardId);
            if (card != null)
            {
                card.Rating++;
                await _cardService.UpdateCard(card);
                return Ok(new { message = "Success" }); // Возвращаем ответ в формате JSON с сообщением об успешном выборе карточки
            }
            else
            {
                return NotFound(new { message = "Card not found" }); // Возвращаем ответ в формате JSON с сообщением о том, что карточка не найдена
            }
        }
    }
}