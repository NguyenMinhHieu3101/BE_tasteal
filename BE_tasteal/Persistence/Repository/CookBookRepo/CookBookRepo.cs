using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.CommentRepo;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BE_tasteal.Persistence.Repository.CookBookRepo
{
    public class CookBookRepo : GenericRepository<CookBookEntity>
    {
        public CookBookRepo(MyDbContext context,
     ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }

        public async Task<List<CookBookRes>> getAllCookBookByUid(String uid)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                select * from cookbook
                where owner = @UID
                ";

                var result = await connection.QueryAsync<CookBookRes>(sql, new { UID = uid });

                return result.ToList();
            }
        }
        public async Task<int> DeleteCookBookRecipeById(String id)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                delete from cookbook_recipe
                where id = @Id
                ";

                var result = await connection.ExecuteAsync(sql, new { Id = id });

                return result;
            }
        }

        public async Task<int> MoveRecipeToNewCookBook(NewRecipeCookBookReq newRecipeCookBook)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                update cookbook_recipe
                set cook_book_id = @NEWBOOKID
                where id = @Id
                ";

                var result = await connection.ExecuteAsync(sql, 
                    new { 
                        NEWBOOKID = newRecipeCookBook.cookbook_id, 
                        Id = newRecipeCookBook.cookbook_recipe_id
                    });

                return result;
            }
        }
        public async Task<int> RenameCookBook(NewCookBookNameReq cookBook )
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                update cookbook
                set name = @NAME
                where id = @Id
                ";

                var result = await connection.ExecuteAsync(sql,
                    new
                    {
                        NAME = cookBook.name,
                        Id = cookBook.id
                    });

                return result;
            }
        }
        public async Task<int> DeleteCookBook(int id)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                delete from cookbook
                where id = @Id
                ";

                var result = await connection.ExecuteAsync(sql, new { Id = id });

                return result;
            }
        }
        public async Task<int> CreateNewCookBook(NewCookBookReq newCookBook)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                insert into cookbook(name, owner)
                values( @NAME, @UID )
                ";

                var result = await connection.ExecuteAsync(sql, new { NAME = newCookBook.name, UID =  newCookBook.owner});

                return result;
            }
        }
        public async Task<int> AddRecipeToCookBook(RecipeToCookBook recipeToCookBook)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                insert into cookbook_recipe(cook_book_id, recipe_id)
                values( @BOOKID, @RECIPEID )
                ";

                var result = await connection.ExecuteAsync(sql, 
                    new { 
                        BOOKID = recipeToCookBook.cook_book_id, 
                        RECIPEID = recipeToCookBook.recipe_id });

                return result;
            }
        }
        public List<CookBook_RecipeEntity> GetCookBookRecipesWithRecipes(int cookBookId)
        {
            return _context.cookBook_RecipeEntities
                .Include(c => c.recipe)
                .Where(c => c.cook_book_id == cookBookId)
                .ToList();             
        }
    }
}
