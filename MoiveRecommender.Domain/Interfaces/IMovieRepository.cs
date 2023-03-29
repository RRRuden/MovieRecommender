using MoiveRecommender.Domain.Entities;

namespace MoiveRecommender.Domain.Interfaces;

public interface IMovieRepository : IRepository<Movie>
{
    Task<List<Movie>> GetRange(int start, int end);

    Task<List<Movie>> GetRatedRange(int userId, int start, int end);

    Task<List<Movie>> GetRangeByName(string name, int start, int end);
}