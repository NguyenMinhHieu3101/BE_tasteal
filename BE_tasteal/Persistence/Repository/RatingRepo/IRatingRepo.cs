using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.RatingRepo
{
    public interface IRatingRepo : IGenericRepository<RatingEntity>
    {
        Task<IEnumerable<RatingRes>?> GetCommentByRecipeId(int recipe_id);
        void CalcRating(int recipe_id);
    }
}
