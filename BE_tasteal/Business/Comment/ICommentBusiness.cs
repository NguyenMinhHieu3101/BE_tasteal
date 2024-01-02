using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.Comment
{
    public interface ICommentBusiness
    {
        Task<IEnumerable<CommentRes>?> GetCommentByRecipeId(int recipe_id);
        Task<CommentEntity?> InsertCommentAsync(int recipe_id, CommentReq req);
        Task<CommentEntity?> UpdateCommentAsync(int recipe_id, int comment_id, CommentReqPut commentUpdate);
        Task<CommentEntity?> DeleteCommentAsync(int recipe_id, int comment_id);
        Task<CommentEntity?> FindAsync(int id);
    }
}
