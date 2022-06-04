using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuizQuot> QuizQuot { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<GameStats> GameStats { get; set; }
    }
}
