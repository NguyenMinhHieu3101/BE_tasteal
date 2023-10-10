namespace BE_tasteal.Business.Nutrition
{
    public interface INutritionBusiness<T, U> where T : class where U : class
    {
        Task<U?> Add(T entity);
    }
}
