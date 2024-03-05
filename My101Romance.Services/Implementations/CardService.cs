using My101Romance.DAL.Interfaces;
using My101Romance.Domain.Entity;
using My101Romance.Domain.Enum;
using My101Romance.Domain.Response;
using My101Romance.Services.Interfaces;

namespace My101Romance.Services.Implementations;

public class CardService : ICardService
{
    public readonly ICardRepository _CardRepository;

    public CardService(ICardRepository cardRepository)
    {
        _CardRepository = cardRepository;
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
                baseResponse.StatusCode = StatusCode.NotFound;
            }

            baseResponse.Data = cards;
            baseResponse.StatusCode = StatusCode.Ok;
            return baseResponse;
        }
        catch( Exception e)
        {
            return new BaseResponse<IEnumerable<Card>>()
            {
                ErrDescription = $"[GetCards]: {e.Message}"
            };
        }
    }
}