using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.CommentRepo
{
    public interface ICommentRepo : IGenericRepository<CommentEntity>
    {
        Task<IEnumerable<CommentRes>> GetCommentByRecipeId(int id);
        List<CommentEntity> getAll(PageReq page);
        CommentEntity? get(int id);
        Task<bool> deleteSoft(int id);
    }
}
