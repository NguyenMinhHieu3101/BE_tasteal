namespace BE_tasteal.Persistence.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> FindByIdAsync(object primaryKey);

        Task<T?> InsertAsync(T entity);

        Task DeleteAsync(T entity, Func<T, bool> predicate = default!);

        Task UpdateAsync(T entity, Func<T, bool> predicate = default!);
    }
}
