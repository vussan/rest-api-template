using System.Linq.Expressions;

namespace Repositories.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool trackChanges = true);
        Task<IEnumerable<TEntity>> GetAll(bool trackChanges=true);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity,bool>> predicate, bool trackChanges=true);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void ExecuteSP(string name);

    }
}