namespace MoiveRecommender.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetById(int id);
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}