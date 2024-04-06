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
            return View(response.Data);
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> GetCard(int id)
    {
        var response = await _cardService.GetCard(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View(response.Data);
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpDelete]
    //[Authorize(Roles = "Admin")]
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
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Save(int id)
    {
        if (id == 0)
        {
            return View();
        }

        var response = await _cardService.GetCard(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View(response.Data);
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
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
            var cards = response.Data;
            return View(cards);
        }
        else
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Top18Plus()
    {
        var response = await _cardService.Top18Plus();
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            var cards = response.Data;
            return View(cards);
        }
        else
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> test()
    {
        var res = await _cardService.AddCard();
        return View(res.Data);
    }
}