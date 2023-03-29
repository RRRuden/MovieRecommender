namespace MoiveRecommender.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IMovieRepository Movie { get; }
    IUserRepository User { get; }
    IRatingRepository Rating { get; }
}