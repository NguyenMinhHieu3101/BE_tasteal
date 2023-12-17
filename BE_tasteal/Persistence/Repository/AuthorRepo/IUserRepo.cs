using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.AuthorRepo
{
    public interface IUserRepo : IGenericRepository<AccountEntity>
    {
        IEnumerable<AuthorRes> AuthorMostRecipe(PageFilter filter);
        Task<AccountEntity> createNewUser(AccountEntity account);
        Task<AccountEntity> updateUser(AccountEntity account);
        Task<IEnumerable<AccountEntity>> getAllUser(PageReq page);
        Task<AccountEntity> getUser(string userId);
    }
}
