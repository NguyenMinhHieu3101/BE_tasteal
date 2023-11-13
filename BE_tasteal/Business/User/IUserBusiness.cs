using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.User
{
    public interface IUserBusiness
    {
        Task<AccountEntity> signup(AccountReq req);
        Task<AccountEntity> udpateAccount(AccountReq req);
        Task<IEnumerable<AccountEntity>> getAllUser();
    }
}
