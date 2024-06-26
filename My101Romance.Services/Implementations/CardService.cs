using My101Romance.DAL.Interfaces;
using My101Romance.Domain.Entity;
using My101Romance.Domain.Enum;
using My101Romance.Domain.Response;
using My101Romance.Domain.ViewModels.Card;
using My101Romance.Services.Interfaces;

namespace My101Romance.Services.Implementations;

public class CardService : ICardService
{
    private readonly ICardRepository _CardRepository;

    public CardService(ICardRepository cardRepository)
    {
        _CardRepository = cardRepository;
    }

    public async Task<IBaseResponse<Card>> GetCard(int id)
    {
        var baseResponse = new BaseResponse<Card>();
        try
        {
            var card = await _CardRepository.Get(id);
            if (card == null)
            {
                baseResponse.ErrDescription = $"Item not found by {id}";
                baseResponse.StatusCode = StatusCode.CardNotFound;
                return baseResponse;
            }

            baseResponse.Data = card;
            baseResponse.StatusCode = StatusCode.Ok;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Card>()
            {
                ErrDescription = $"[GetCard]: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }



    public async Task<IBaseResponse<IEnumerable<Card>>> GetCards()
    {
        var baseResponse = new BaseResponse<IEnumerable<Card>>();
        try
        {
            var cards = await _CardRepository.Select();
            if (cards.Count == 0)
            {
                
                baseResponse.ErrDescription = "Found 0 elements";
                baseResponse.StatusCode = StatusCode.CardNotFound;
            }

            baseResponse.Data = cards!;
            baseResponse.StatusCode = StatusCode.Ok;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<Card>>()
            {
                ErrDescription = $"[GetCards]: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    
    public async Task<IBaseResponse<Card>> GetCardByTitle(string title)
    {
        var baseResponse = new BaseResponse<Card>();
        try
        {
            var card = await _CardRepository.GetByTitle(title);
            if (card == null)
            {
                baseResponse.ErrDescription = $"Item not found by `{title}`";
                baseResponse.StatusCode = StatusCode.CardNotFound;
                return baseResponse;
            }

            baseResponse.Data = card;
            baseResponse.StatusCode = StatusCode.Ok;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Card>()
            {
                ErrDescription = $"[GetCardByTitle]: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<CardViewModel>> CreateCard(CardViewModel cardViewModel)
    {
        var baseResponse = new BaseResponse<CardViewModel>();
        try
        {
            /*var card = new Card
            {
                Title = cardViewModel.Title,
                Description = cardViewModel.Description,
                ImagePath = cardViewModel.,
                IsForAll = cardViewModel.IsForAll,
                Rating = cardViewModel.Rating
            };

            await _CardRepository.Create(card);
            baseResponse.StatusCode = StatusCode.Ok;*/
        }
        catch (Exception e)
        {
            return new BaseResponse<CardViewModel>
            {
                ErrDescription = $"[CreateCard]: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }

        return baseResponse;
    }


    public async Task<IBaseResponse<bool>> DeleteCard(int id)
    {
        var baseResponse = new BaseResponse<bool>()
        {
            Data = true
        };
        try
        {
            var card = await _CardRepository.Get(id);
            if (card == null)
            {
                baseResponse.ErrDescription = $"Item not found by `{id}`";
                baseResponse.StatusCode = StatusCode.CardNotFound;
                baseResponse.Data = false;
                return baseResponse;
            }

            await _CardRepository.Delete(card);

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                ErrDescription = $"[DeleteCard]: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public Task<IBaseResponse<Card>> GetByTitle(string title)
    {
        throw new NotImplementedException();
    }

    public async Task<IBaseResponse<Card>> Edit(int id, CardViewModel model)
    {
        var baseResponse = new BaseResponse<Card>();
        try
        {
            var card = await _CardRepository.Get(id);
            if (card == null)
            {
                baseResponse.StatusCode = StatusCode.CardNotFound;
                baseResponse.ErrDescription = "Card not found";

                return baseResponse;
            }

            card.Title = model.Title;
            card.Description = model.Description;
            card.Rating = model.Rating;
            card.IsForAll = model.IsForAll;

            await _CardRepository.Update(card);

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Card>()
            {
                ErrDescription = $"[Edit]: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<Card>>> GetRandomCards()
    {
        var baseResponse = new BaseResponse<IEnumerable<Card>>();
        try
        {
            Random random = new Random();
            var randomCards = await _CardRepository.SelectEightCards();

            if (randomCards.Count() == 0)
            {
                baseResponse.ErrDescription = "Found 0 elements";
                baseResponse.StatusCode = StatusCode.CardNotFound;
            }
            else
            {
                baseResponse.Data = randomCards;
                baseResponse.StatusCode = StatusCode.Ok;
            }
        }
        catch (Exception e)
        {
            baseResponse.ErrDescription = $"[GetRandomCards]: {e.Message}";
            baseResponse.StatusCode = StatusCode.InternalServerError;
        }

        return baseResponse;
    }

    public async Task<IBaseResponse<IEnumerable<Card>>> Top()
    {
        var baseResponse = new BaseResponse<IEnumerable<Card>>();
        try
        {
            var cards = await _CardRepository.GetTop();
            if (cards == null || !cards.Any())
            {
                baseResponse.ErrDescription = "Found 0 elements";
                baseResponse.StatusCode = StatusCode.CardNotFound;
            }
            else
            {
                baseResponse.Data = cards;
                baseResponse.StatusCode = StatusCode.Ok;
            }
        }
        catch(Exception e)
        {
            baseResponse.ErrDescription = $"[Top]: {e.Message}";
            baseResponse.StatusCode = StatusCode.InternalServerError;
        }

        return baseResponse;
    }
    
    public async Task<IBaseResponse<IEnumerable<Card>>> Top18Plus()
    {
        var baseResponse = new BaseResponse<IEnumerable<Card>>();
        try
        {
            var cards = await _CardRepository.GetTop18Plus();
            if (cards == null || !cards.Any())
            {
                baseResponse.ErrDescription = "Found 0 elements";
                baseResponse.StatusCode = StatusCode.CardNotFound;
            }
            else
            {
                baseResponse.Data = cards;
                baseResponse.StatusCode = StatusCode.Ok;
            }
        }
        catch(Exception e)
        {
            baseResponse.ErrDescription = $"[Top18plus]: {e.Message}";
            baseResponse.StatusCode = StatusCode.InternalServerError;
        }

        return baseResponse;
    }
    
    public async Task<List<Card>> GetEightCards()
    {
        return await _CardRepository.SelectEightCards();

    }

    public async Task<Card> GetCardById(int cardId)
    {
        return await _CardRepository.GetCardById(cardId);
    }

    public async Task UpdateCard(Card card)
    {
        await _CardRepository.UpdateCard(card);
    }

    public async Task<IBaseResponse<Card>> AddCard()
    {
        var baseResponse = new BaseResponse<Card>();
        try
        {
            Random r = new Random();
            var card = new Card()
            {
                
                Title = "Кінотеатр",
                Description = "Побачення в кінотеатрі - це спільний перегляд фільму, що створює затишну атмосферу та дає можливість обговорити враження після сеансу.",
                IsForAll = true,
                Rating = 0

            };

            await _CardRepository.Create(card);
            


            baseResponse.Data = card;
            baseResponse.StatusCode = StatusCode.Ok;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Card>()
            {
                ErrDescription = $"[AddCard]: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
        
        
    }
}