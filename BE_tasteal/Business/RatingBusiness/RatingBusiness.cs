using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.RatingRepo;
using BE_tasteal.Persistence.Repository.RecipeRepo;

namespace BE_tasteal.Business.RatingBusiness
{
    public class RatingBusiness : IRatingBusiness
    {
        private readonly IRatingRepo _ratingRepo;
        private readonly IRecipeRepository _recipeRepository;
        public RatingBusiness(
            IRatingRepo commentRepo,
            IRecipeRepository recipeRepository)
        {
            _ratingRepo = commentRepo;
            _recipeRepository = recipeRepository;
        }
        public async Task<IEnumerable<RatingRes>?> GetCommentByRecipeId(int recipe_id)
        {
            if (await _recipeRepository.FindByIdAsync(recipe_id) == null)
            {
                return null;
            }
            var result = await _ratingRepo.GetCommentByRecipeId(recipe_id);
            return result;
        }
        public async Task<RatingEntity?> InsertCommentAsync(int recipe_id, RatingReq req)
        {
            RatingEntity commentEntity = new RatingEntity
            {
                recipe_id = recipe_id,
                account_id = req.account_id,
                rating = req.rating,
            };
            var result = await _ratingRepo.InsertAsync(commentEntity);
            _ratingRepo.CalcRating(recipe_id);
            return result;
        }
        public async Task<RatingEntity?> FindAsync(int id)
        {
            return await _ratingRepo.FindByIdAsync(id);
        }
        public async Task<RatingEntity?> UpdateCommentAsync(int recipe_id, int rating_id, decimal rating)
        {
            var result = await _ratingRepo.FindByIdAsync(rating_id);
            result.rating = rating;
            await _ratingRepo.UpdateAsync(result);
            _ratingRepo.CalcRating(recipe_id);
            return result;
        }
        public async Task<RatingEntity?> DeleteCommentAsync(int recipe_id, int comment_id)
        {
            var result = await _ratingRepo.FindByIdAsync(comment_id);
            if (result != null)
            {
                await _ratingRepo.DeleteAsync(result);
            }
            _ratingRepo.CalcRating(recipe_id);
            return result;
        }
    }
}
