using MoiveRecommender.Domain.Interfaces;
using MovieRecommender.DAL.Repositories;

namespace MovieRecommender.DAL.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieDbContext _context;

    public UnitOfWork(MovieDbContext context)
    {
        _context = context;
        Movie = new MovieRepository(_context);
        User = new UserRepository(_context);
        Rating = new RatingRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IMovieRepository Movie { get; }
    public IUserRepository User { get; }
    public IRatingRepository Rating { get; }
}