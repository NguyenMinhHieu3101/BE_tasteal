using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.CommentRepo;
using BE_tasteal.Persistence.Repository.RecipeRepo;

namespace BE_tasteal.Business.Comment
{
    public class CommentBusiness : ICommentBusiness
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IRecipeRepository _recipeRepository;
        public CommentBusiness(
            ICommentRepo commentRepo,
            IRecipeRepository recipeRepository)
        {
            _commentRepo = commentRepo;
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<CommentRes>?> GetCommentByRecipeId(int recipe_id)
        {
            if (await _recipeRepository.FindByIdAsync(recipe_id) == null) {
                return null;
            }
            var result = await _commentRepo.GetCommentByRecipeId(recipe_id);
            return result;
        }

        public async Task<CommentEntity?> InsertCommentAsync(int recipe_id, CommentReq req)
        {
            CommentEntity commentEntity = new CommentEntity
            {
                recipe_id = recipe_id,
                account_id = req.account_id,
                comment = req.comment,
            };
            var result = await _commentRepo.InsertAsync(commentEntity);
            return result;
        }
        public async Task<CommentEntity?> FindAsync(int id)
        {
            return await _commentRepo.FindByIdAsync(id);
        }
        public async Task<CommentEntity?> UpdateCommentAsync(int recipe_id, int comment_id, string commentUpdate)
        {
            var result = await _commentRepo.FindByIdAsync(comment_id);
            result.comment = commentUpdate;
            await _commentRepo.UpdateAsync(result);
            return result;
        }

        public async Task<CommentEntity?> DeleteCommentAsync(int recipe_id, int comment_id)
        {
            var result = await _commentRepo.FindByIdAsync(comment_id);
            if(result != null)
            {
                await _commentRepo.DeleteAsync(result);
            }
            return result;
        }
    }
}
