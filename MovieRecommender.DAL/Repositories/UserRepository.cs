using Microsoft.EntityFrameworkCore;
using MoiveRecommender.Domain.Entities;
using MoiveRecommender.Domain.Interfaces;
using MovieRecommender.DAL.Data;

namespace MovieRecommender.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MovieDbContext _context;

    public UserRepository(MovieDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task Create(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByLogin(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Login == login);
    }
}