using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.RatingBusiness
{
    public interface IRatingBusiness
    {
        Task<IEnumerable<RatingRes>?> GetCommentByRecipeId(int recipe_id);
        Task<RatingEntity?> InsertCommentAsync(int recipe_id, RatingReq req);
        Task<RatingEntity?> FindAsync(int id);
        Task<RatingEntity?> UpdateCommentAsync(int recipe_id, int rating_id, decimal rating);
        Task<RatingEntity?> DeleteCommentAsync(int recipe_id, int comment_id);
    }
}
