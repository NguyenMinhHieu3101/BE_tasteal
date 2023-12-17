using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;

namespace BE_tasteal.Business.User
{
    public class UserBusiness: IUserBusiness
    {
        private IUserRepo _accountRepo;
        public UserBusiness(IUserRepo authorRepo)
        {
            _accountRepo = authorRepo;
        }
        public async Task<AccountEntity> signup(AccountReq req)
        {
            AccountEntity accountEntity = new AccountEntity();
            accountEntity.uid = req.uid;
            accountEntity.name = req.name;
            accountEntity.avatar = req.avatar;
            accountEntity.introduction = req.introduction;
            accountEntity.slogan = req.slogan;
            accountEntity.link = req.link;
            accountEntity.quote = req.quote;

            return await _accountRepo.createNewUser(accountEntity);
        }

        public async Task<AccountEntity> udpateAccount(AccountReq req)
        {
            AccountEntity accountEntity = new AccountEntity();
            accountEntity.uid = req.uid;
            accountEntity.name = req.name;
            accountEntity.avatar = req.avatar;
            accountEntity.introduction = req.introduction;
            accountEntity.slogan = req.slogan;
            accountEntity.quote = req.quote;
            accountEntity.link = req.link;

            return await _accountRepo.updateUser(accountEntity);
        }
        public async Task<IEnumerable<AccountEntity>> getAllUser()
        {
            return await _accountRepo.getAllUser();
        }
        public async Task<AccountEntity> getUser(string userId)
        {
            return await _accountRepo.getUser(userId);
        }
    }
}
