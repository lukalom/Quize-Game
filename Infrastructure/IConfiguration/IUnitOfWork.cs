using Infrastructure.Entities;
using Infrastructure.Repository;

namespace Infrastructure.IConfiguration
{
    public interface IUnitOfWork
    {
        public IGenericRepository<User> User { get; }
        public IGenericRepository<Quote> Quote { get; }
        public IGenericRepository<Quiz> Quiz { get; }
        public IGenericRepository<GameStats> GameStats { get; }
        public IGenericRepository<QuizQuot> QuizQuot { get; }

        Task SaveAsync();
    }
}
