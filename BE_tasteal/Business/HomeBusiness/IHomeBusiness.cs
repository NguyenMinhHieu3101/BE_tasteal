using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.HomeBusiness
{
    public interface IHomeBusiness
    {
        Task<List<OccasionEntity>> GetAllOccasion();
        Task<List<RecipeEntity>> GetRecipeByTime();
        Task<List<RecipeEntity>> GetRecipeByRating();
        Task<List<AccountEntity>> GetAuthor();
    }
}
