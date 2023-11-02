using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;

namespace BE_tasteal.Persistence.Repository.AuthorRepo
{
    public class AuthorRepo : GenericRepository<AccountEntity>, IAuthorRepo
    {
        public AuthorRepo(MyDbContext context,
         ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }

        public IEnumerable<AuthorRes> AuthorMostRecipe(PageFilter filter)
        {
            string sortOrder = filter.isDescend ? "DESC" : "ASC";
            int pageSize = filter.pageSize;
            int page = filter.page;
            int offset = (page - 1) * pageSize;

            string sqlQuery = @"
                    SELECT a.id, a.name, a.avatar, a.introduction, COUNT(r.id) as RecipeCount
                    FROM account a, recipe r
		            where a.id = r.author
                    GROUP BY a.id
                    ORDER BY RecipeCount DESC";
            using (var connection = _connection.GetConnection())
            {
                var accounts = connection.Query<AuthorRes>(sqlQuery, new { Offset = offset, PageSize = pageSize });
                return accounts;
            }
        }
    }
}
