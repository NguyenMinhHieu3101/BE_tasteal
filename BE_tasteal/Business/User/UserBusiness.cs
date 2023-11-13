using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;

namespace BE_tasteal.Business.User
{
    public class UserBusiness: IUserBusiness
    {
        private IAuthorRepo _authorRepo;
        public UserBusiness(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
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

            return await _authorRepo.createNewUser(accountEntity);
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

            return await _authorRepo.updateUser(accountEntity);
        }
        public async Task<IEnumerable<AccountEntity>> getAllUser()
        {
            return await _authorRepo.getAllUser();
        }
    }
}
