using Infrastructure.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class, IEntity, IPrimaryKey
    {
        protected readonly ApplicationDbContext Db;

        public GenericRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public async ValueTask<T?> Find(int id, bool isDisabled = false) => await Db.Set<T>().SingleOrDefaultAsync(x => x.Id == id && x.IsDisabled == isDisabled);

        public IQueryable<T> Query(Expression<Func<T, bool>>? expression = null, bool isDisabled = false)
        {
            var baseQuery = Db.Set<T>().Where(x => x.IsDisabled == isDisabled);

            return expression != null ? baseQuery.Where(expression) : baseQuery;
        }

        public async ValueTask AddAsync(T entity)
        {
            await Db.Set<T>().AddAsync(entity);
        }

        public async ValueTask AddRangeAsync(IEnumerable<T> entity)
        {
            await Db.Set<T>().AddRangeAsync(entity);
        }

        public void Remove(T entity)
        {
            Db.Set<T>().Remove(entity);
        }
    }
}
