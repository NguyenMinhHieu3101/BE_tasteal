using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.CommentRepo
{
    public interface ICommentRepo : IGenericRepository<CommentEntity>
    {
        IEnumerable<CommentRes> GetCommentByRecipeId(int id);
    }
}
