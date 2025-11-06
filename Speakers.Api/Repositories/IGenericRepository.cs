using Microsoft.EntityFrameworkCore;

namespace Speakers.Api.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IList<TEntity> Get();

        Task<TEntity> GetByIdAsync(string id);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Create(TEntity entity);

        IEnumerable<TEntity> Search(Func<TEntity, bool> predicate);

        Task<TEntity> Delete(string id);

        Task SaveAsync();
    }


    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IList<TEntity> Get()
        {
            DbSet<TEntity> dbSet = _dbContext.Set<TEntity>();

            return dbSet.ToList();
        }

        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual IEnumerable<TEntity> Search(Func<TEntity, bool> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Set<TEntity>();

            _dbContext.Entry(entity).State = EntityState.Modified;

            await SaveAsync();

            return entity;
        }

        public virtual async Task<TEntity> Delete(string id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            _dbContext.Set<TEntity>().Remove(entity);

            _dbContext.SaveChanges();

            return entity;
        }

        public virtual async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
