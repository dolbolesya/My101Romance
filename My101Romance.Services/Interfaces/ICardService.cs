using My101Romance.Domain.Entity;
using My101Romance.Domain.Response;

namespace My101Romance.Services.Interfaces;

public interface ICardService
{
    Task<IBaseResponse<IEnumerable<Card>>> GetCards();
}