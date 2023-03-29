using Microsoft.EntityFrameworkCore;
using MoiveRecommender.Domain.Entities;

namespace MovieRecommender.DAL.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie?> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
