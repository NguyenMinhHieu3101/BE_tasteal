using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;

namespace BE_tasteal.Business.User
{
    public class UserBusiness : IUserBusiness
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
            accountEntity.isDeleted = req.isDeleted;

            return await _accountRepo.createNewUser(accountEntity);
        }

        public async Task<AccountEntity> udpateAccount(AccountEntity req)
        {
            var account = await _accountRepo.FindByIdAsync(req.uid);
            account.name = req.name;
            account.avatar = req.avatar;
            account.introduction = req.introduction;
            account.link = req.link;
            account.quote = req.quote;
            account.isDeleted = req.isDeleted;

            await _accountRepo.UpdateAsync(account);

            return account;
        }
        public async Task<IEnumerable<AccountEntity>> getAllUser(PageReq page)
        {
            return await _accountRepo.getAllUser(page);
        }
        public async Task<AccountEntity?> getUser(string userId)
        {
            return await _accountRepo.getUser(userId);
        }
    }
}
