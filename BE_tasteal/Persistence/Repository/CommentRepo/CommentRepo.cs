using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace BE_tasteal.Persistence.Repository.CommentRepo
{
    public class CommentRepo : GenericRepository<CommentEntity>, ICommentRepo
    {
        public CommentRepo(MyDbContext context,
       ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
        public async Task<IEnumerable<CommentRes>> GetCommentByRecipeId(int id)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                Select comment.id as id, account.uid as account_id , account.name as name , comment.comment , comment.image,
                DATE_FORMAT(comment.created_at, '%Y-%m-%dT%H:%i:%s.000Z') as created_at,
                DATE_FORMAT(comment.updated_at, '%Y-%m-%dT%H:%i:%s.000Z') as updated_at
                from comment, account
                where comment.account_id = account.uid
                and comment.recipe_id = @Id
                ";

                var result = await connection.QueryAsync<CommentRes>(sql, new { Id = id });

                return result;
            }
        }
        public List<CommentEntity> getAll(PageReq page)
        {
            return _context.Comment.Include(c => c.Account).Take(page.pageSize).Skip((page.page - 1) * page.pageSize).AsList();
        }
        public CommentEntity? get(int id)
        {
            return _context.Comment.Where(c => c.id == id).FirstOrDefault();
        }
        public async Task<bool> deleteSoft(int id)
        {
            try
            {
                var cmt = await FindByIdAsync(id);

                if (cmt != null) cmt.isDeleted = true;

                _context.Set<CommentEntity>().Update(cmt);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
