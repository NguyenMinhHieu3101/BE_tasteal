using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.HomeBusiness
{
    public interface IHomeBusiness
    {
        IEnumerable<OccasionEntity> GetAllOccasion();
        IEnumerable<RecipeEntity> GetRecipeByTime(PageFilter filter);
        IEnumerable<RecipeEntity> GetRecipeByRating(PageFilter filter);
        IEnumerable<AuthorRes> GetAuthor(PageFilter filter);
    }
}
