using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.AuthorRepo
{
    public class AuthorRepo : GenericRepository<AccountEntity>, IAuthorRepo
    {
        public AuthorRepo(MyDbContext context,
         ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
    }
}
