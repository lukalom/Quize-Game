using Infrastructure.Entities;
using Infrastructure.IConfiguration;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _db;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(ApplicationDbContext db, IServiceProvider serviceProvider)
        {
            _db = db;
            _serviceProvider = serviceProvider;
        }

        public IGenericRepository<User> User => _serviceProvider.GetRequiredService<IGenericRepository<User>>();
        public IGenericRepository<Quote> Quote => _serviceProvider.GetRequiredService<IGenericRepository<Quote>>();
        public IGenericRepository<Quiz> Quiz => _serviceProvider.GetRequiredService<IGenericRepository<Quiz>>();
        public IGenericRepository<GameStats> GameStats => _serviceProvider.GetRequiredService<IGenericRepository<GameStats>>();
        public IGenericRepository<QuizQuot> QuizQuot => _serviceProvider.GetRequiredService<IGenericRepository<QuizQuot>>();

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
