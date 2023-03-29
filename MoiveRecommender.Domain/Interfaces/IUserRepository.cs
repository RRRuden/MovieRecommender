using MoiveRecommender.Domain.Entities;

namespace MoiveRecommender.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<IEnumerable<User>> GetUsers();

    public Task<User> GetByLogin(string login);
}