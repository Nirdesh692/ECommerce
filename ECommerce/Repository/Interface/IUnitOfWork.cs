using ECommerce.Data;
using ECommerce.Models;

namespace ECommerce.Repository.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> CompleteAsync();
        public void Dispose();
    }
}
