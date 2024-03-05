using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using My101Romance.DAL.Interfaces;
using My101Romance.Domain.Entity;
using My101Romance.Models;
using My101Romance.Services.Interfaces;
using My101Romance.Domain.Enum;
using My101Romance.Domain.Response;


namespace My101Romance.Controllers;

public class CardController : Controller
{
    private readonly ICardService _cardService;
    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetCards()
    {
        var response = await _cardService.GetCards();
        return View(response.Data);
    }

}