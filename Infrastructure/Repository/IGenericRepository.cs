using Infrastructure.Abstractions;
using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public interface IGenericRepository<T>
        where T : class, IEntity, IPrimaryKey
    {
        ValueTask<T?> Find(int id, bool isDisabled = false);
        IQueryable<T> Query(Expression<Func<T, bool>>? expression = null, bool isDisabled = false);
        ValueTask AddRangeAsync(IEnumerable<T> entity);
        ValueTask AddAsync(T entity);
        void Remove(T entity);
    }
}
