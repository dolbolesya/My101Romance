using My101Romance.Domain.Entity;
using My101Romance.Domain.Response;
using My101Romance.Domain.ViewModels.Card;

namespace My101Romance.Services.Interfaces;

public interface ICardService
{
    Task<IBaseResponse<IEnumerable<Card>>> GetCards();

    Task<IBaseResponse<Card>> GetCard(int id);

    Task<IBaseResponse<CardViewModel>> CreateCard(CardViewModel cardViewModel);

    Task<IBaseResponse<bool>> DeleteCard(int id);

    Task<IBaseResponse<Card>> GetByTitle(string title);
    Task<IBaseResponse<Card>> Edit(int id, CardViewModel model);
}