using System.Linq.Expressions;

namespace ECommerce.Repository.Interface
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> Queryable();
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);  
        Task Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}