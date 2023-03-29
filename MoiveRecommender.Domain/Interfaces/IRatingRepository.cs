using MoiveRecommender.Domain.Entities;

namespace MoiveRecommender.Domain.Interfaces;

public interface IRatingRepository : IRepository<Rating>
{
    Task<IEnumerable<Rating>> GetRange(int start, int end);

    Task<Rating> Get(int userId, int movieId);
}