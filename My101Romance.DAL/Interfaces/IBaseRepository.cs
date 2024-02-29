using My101Romance.Domain.Entity;

namespace My101Romance.DAL.Interfaces;

public interface IBaseRepository<T>
{
   Task<bool> Create(T entity);

    Task<Card?> Get(int id);

    Task<List<Card?>> Select();

    Task<bool> Delete(T entity);
}