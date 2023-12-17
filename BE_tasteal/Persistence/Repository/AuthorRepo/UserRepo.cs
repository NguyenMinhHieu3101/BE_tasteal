using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using MySqlConnector;
using System.Security.Principal;

namespace BE_tasteal.Persistence.Repository.AuthorRepo
{
    public class UserRepo : GenericRepository<AccountEntity>, IUserRepo
    {
        public UserRepo(MyDbContext context,
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
                    SELECT a.uid, a.name, a.avatar, a.introduction, a.link, a.slogan, a.quote, COUNT(r.id) as RecipeCount
                    FROM account a, recipe r
		            where a.uid = r.author
                    GROUP BY a.uid
                    ORDER BY RecipeCount DESC";
            using (var connection = _connection.GetConnection())
            {
                var accounts = connection.Query<AuthorRes>(sqlQuery, new { Offset = offset, PageSize = pageSize });
                return accounts;
            }
        }
        public async Task<AccountEntity> createNewUser(AccountEntity newAccount)
        {
            using (var connection = _connection.GetConnection())
            {

                var insertQuery = "INSERT INTO Account (uid, name, avatar, introduction, link, slogan, quote) " +
                                  "VALUES (@uid, @name, @avatar, @introduction, @link, @slogan, @quote);" + 
                                  " Insert into cookbook(name, owner) values ('Yêu thích', @uid) "
                                  ;

                connection.Execute(insertQuery, newAccount);

                var createdAccount = connection.QuerySingleOrDefault<AccountEntity>("SELECT * FROM Account WHERE uid = @uid", new { newAccount.uid });

                return createdAccount;
            }
        }
        public async Task<AccountEntity> updateUser(AccountEntity account)
        {
            using (var connection = _connection.GetConnection())
            {

                var updateQuery = "UPDATE Account SET ";

                if (!string.IsNullOrEmpty(account.name))
                {
                    updateQuery += "name = @name, ";
                }

                if (!string.IsNullOrEmpty(account.avatar))
                {
                    updateQuery += "avatar = @avatar, ";
                }

                if (!string.IsNullOrEmpty(account.introduction))
                {
                    updateQuery += "introduction = @introduction, ";
                }

                if (!string.IsNullOrEmpty(account.link))
                {
                    updateQuery += "link = @link, ";
                }

                if (!string.IsNullOrEmpty(account.slogan))
                {
                    updateQuery += "slogan = @slogan, ";
                }

                if (!string.IsNullOrEmpty(account.quote))
                {
                    updateQuery += "quote = @quote, ";
                }

                // Loại bỏ dấu phẩy cuối cùng và thêm điều kiện WHERE cho trường uid
                updateQuery = updateQuery.TrimEnd(',', ' ') + " WHERE uid = @uid";

                // Thực hiện câu lệnh SQL với tham số từ đối tượng account
                var result = await connection.ExecuteAsync(updateQuery, account);

                var updatedAccount = connection.QuerySingleOrDefault<AccountEntity>("SELECT * FROM Account WHERE uid = @uid", account);
                return updatedAccount;
            }
        }
        public async Task<IEnumerable<AccountEntity>> getAllUser(PageReq pageReq)
        {
            int pageSize = pageReq.pageSize;
            int page = pageReq.page;
            int offset = (page - 1) * pageSize;
            using (var connection = _connection.GetConnection())
            {
               
                var insertQuery = "Select * from account LIMIT @offset, @pageSize";

                var accounts = await connection.QueryAsync<AccountEntity>(insertQuery, new
                {
                    offset = offset,
                    pageSize = pageSize,
                });

                return accounts;
            }
        }
        public async Task<AccountEntity?> getUser(string userId)
        {
            //return await _context.Set<AccountEntity>().FindAsync(userId);
            using (var connection = _connection.GetConnection())
            {
                var query = "select * from account where uid = @UID";

                var result = await connection.QueryFirstOrDefaultAsync<AccountEntity>(query, new
                {
                    UID = userId
                });
                return result;
            }
        }
    }
}
