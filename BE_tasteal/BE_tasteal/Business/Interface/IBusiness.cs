namespace BE_tasteal.Business.Interface
{
    public interface IBusiness<T, U> where T : class where U : class
    {
        Task<List<U?>> GetAll();

        Task<U?> Add(T entity);
    }
}
