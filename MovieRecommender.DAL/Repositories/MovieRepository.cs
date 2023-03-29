using Microsoft.EntityFrameworkCore;
using MoiveRecommender.Domain.Entities;
using MoiveRecommender.Domain.Interfaces;
using MovieRecommender.DAL.Data;

namespace MovieRecommender.DAL.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieDbContext _context;

    public MovieRepository(MovieDbContext context)
    {
        _context = context;
    }

    public async Task<Movie> GetById(int id)
    {
        return await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
    }

    public async Task Create(Movie entity)
    {
        await _context.Movies.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Movie entity)
    {
        _context.Movies.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Movie entity)
    {
        _context.Movies.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Movie>> GetRange(int start, int end)
    {
        return await _context.Movies.OrderBy(x=>x.Id).Take(end).Skip(start).ToListAsync();
    }

    public async Task<List<Movie>> GetRatedRange(int userId, int start, int end)
    {
        return (await _context
            .Ratings
            .Where(x => x.UserId == userId)?
            .Select(x => _context.Movies.FirstOrDefault(m => m.Id == x.MovieId))
            .OrderBy(x => x.Id)
            .Take(end).Skip(start)
            .ToListAsync()!)!;
    }

    public async Task<List<Movie>> GetRangeByName(string name, int start, int end)
    {
        return await _context.Movies
            .Where(m => m.Title.ToLower().Contains(name.ToLower()))
            .OrderBy(x => x.Id)
            .Take(end).Skip(start)
            .ToListAsync();
    }
}