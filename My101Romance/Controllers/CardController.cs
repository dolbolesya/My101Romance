using Microsoft.AspNetCore.Mvc;
using My101Romance.DAL.Interfaces;
using My101Romance.Domain.Entity;

namespace My101Romance.Controllers;

public class CardController : Controller
{
    private readonly ICardRepository _cardRepository;
    
    public CardController(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCards()
    {
        var card = new Card()
        {
            Title = "test title",
            Description = "test desc",
            Rating = 0,
            IsForAll = true
        };
        
        await _cardRepository.Create(card);
        
        var response = await _cardRepository.Select();
        return View(response);
    }

}