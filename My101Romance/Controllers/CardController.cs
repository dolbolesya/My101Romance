using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My101Romance.Services.Interfaces;
using My101Romance.Domain.ViewModels.Card;
using My101Romance.Controllers;
using My101Romance.Domain.Entity;


namespace My101Romance.Controllers;

public class CardController(ICardService cardService) : Controller
{
    private readonly ICardService _cardService = cardService;


    
    [HttpGet]
    public async Task<IActionResult> GetCards()
    {
        var response = await _cardService.GetCards();
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View("dev/GetCards",response.Data);
        }

        return RedirectToAction("Error", "Home");
    }

    [Authorize(Roles = "root, admin")]
    [HttpGet]
    public async Task<IActionResult> GetCard(int id)
    {
        var response = await _cardService.GetCard(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View("dev/GetCard",response.Data);
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpDelete]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _cardService.DeleteCard(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return RedirectToAction("GetCards");
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Save(int id)
    {
        if (id == 0)
        {
            return View("dev/Save");
        }

        var response = await _cardService.GetCard(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View("dev/Save",response.Data);
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Save(CardViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Id == 0)
            {
               await _cardService.CreateCard(model);
            }
            else
            {
                await _cardService.Edit(model.Id, model);
            }
        }

        return RedirectToAction("GetCards");
    }


    [HttpGet]
    public async Task<IActionResult> ShowRandomCards()
    {
        var response = await _cardService.GetRandomCards();
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            var randomCards = response.Data;
            return View(randomCards);
        }
        else
        {
            // Обработка ошибки, например, перенаправление на страницу с ошибкой или другие действия
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Top()
    {
        var response = await _cardService.Top();
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            var cards = response.Data.ToList();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Top18Plus");
            }
            else
            {
                return View("top/Top", cards);
            }
        }
        else
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Top18Plus()
    {
        var response = await _cardService.Top18Plus();
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            var cards = response.Data;
            return View("top/Top18Plus", cards);
        }
        else
        {
            return RedirectToAction("Error", "Home");
        }
    }

    //[Authorize(Roles = "root, admin")]
    [HttpGet]
    public async Task<IActionResult> AddCard()
    {
        var res = await _cardService.AddCard();
        return View("dev/test",res.Data);
    }
    
    [Authorize(Roles = "root, admin")]
    public async Task<IActionResult> GetEightCards()
    {
        List<Card> cards = await _cardService.GetEightCards();
        return View("dev/GetEightCards", cards);
    }
    


}