using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Core;
using System.Linq.Expressions;

namespace Repositories.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool trackChanges = true)
        {
            return await _context.Set<TEntity>().FirstAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAll(bool trackChanges=true)
        {
            return trackChanges ? await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, bool trackChanges=true)
        {
            return trackChanges ? await _context.Set<TEntity>().Where(predicate).ToListAsync()
                : await _context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public async void Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }


        public void ExecuteSP(string name)
        {
            _context.Database.ExecuteSqlRawAsync(name);
        }


    }
}