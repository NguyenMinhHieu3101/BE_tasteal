using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Interface.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace BE_tasteal.Persistence.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        public readonly DbContext _context;
        public readonly ConnectionManager _connection;

        public GenericRepository(DbContext context,
             ConnectionManager connectionManager)
        {
            _context = context;
            _connection = connectionManager;
        }


        public async Task<T?> FindByIdAsync(object primaryKey)
        {
            var entity = await _context.Set<T>().FindAsync(primaryKey);

            return entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            _context.Attach(entity);
            var entityEntry = await _context.Set<T>().AddAsync(entity);

            await SaveDatabaseAsync();

            return entityEntry.Entity;
        }

        public async Task UpdateAsync(T entity, Func<T, bool> predicate = default!)
        {
            if (predicate != default)
            {
                DetachLocal(predicate);
            }

            _context.Set<T>().Update(entity);

            await SaveDatabaseAsync();
        }
        public async Task DeleteAsync(T entity, Func<T, bool> predicate = default!)
        {
            if (predicate != default)
            {
                DetachLocal(predicate);
            }

            _context.Set<T>().Remove(entity);

            await SaveDatabaseAsync();
        }
        private async Task SaveDatabaseAsync()
        {
            await _context.SaveChangesAsync();
        }

        private void DetachLocal(Func<T, bool> predicate)
        {
            var local = _context.Set<T>().Local.FirstOrDefault(predicate);

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}
