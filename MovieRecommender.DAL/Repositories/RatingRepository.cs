using Microsoft.EntityFrameworkCore;
using MoiveRecommender.Domain.Entities;
using MoiveRecommender.Domain.Interfaces;
using MovieRecommender.DAL.Data;

namespace MovieRecommender.DAL.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly MovieDbContext _context;

    public RatingRepository(MovieDbContext context)
    {
        _context = context;
    }

    public async Task<Rating> GetById(int id)
    {
        return await _context.Ratings.FirstOrDefaultAsync(rating => rating.Id == id);
    }

    public async Task Create(Rating entity)
    {
        await _context.Ratings.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Rating entity)
    {
        _context.Ratings.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Rating entity)
    {
        _context.Ratings.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Rating>> GetRange(int start, int end)
    {
        return await _context.Ratings
            .Take(end).Skip(start).ToListAsync();
    }

    public async Task<Rating> Get(int userId, int movieId)
    {
        return await _context.Ratings.FirstOrDefaultAsync(rating =>
            rating.MovieId == movieId && rating.UserId == userId);
    }
}