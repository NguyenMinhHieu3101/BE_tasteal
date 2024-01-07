using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

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
            var newCookBook = _context.CookBook_Recipe
                .Where(c => c.id == newRecipeCookBook.cookbook_recipe_id
                && c.cook_book_id == newRecipeCookBook.cookbook_id);

            if (newCookBook != null)
            {
                return 1;
            }

            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                update cookbook_recipe
                set cook_book_id = @NEWBOOKID
                where id = @Id
                ";

                var result = await connection.ExecuteAsync(sql,
                    new
                    {
                        NEWBOOKID = newRecipeCookBook.cookbook_id,
                        Id = newRecipeCookBook.cookbook_recipe_id
                    });

                return result;
            }
        }
        public async Task<int> RenameCookBook(NewCookBookNameReq cookBook)
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

                var result = await connection.ExecuteAsync(sql, new { NAME = newCookBook.name, UID = newCookBook.owner });

                return result;
            }
        }
        public async Task<int> AddRecipeToCookBook(RecipeToCookBook recipeToCookBook)
        {
            var recipeInCookBook = _context.CookBook_Recipe.Where(
                c => c.recipe_id == recipeToCookBook.recipe_id
                && c.cook_book_id == recipeToCookBook.cook_book_id);

            if (recipeInCookBook != null)
            {
                return 1;
            }

            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                insert into cookbook_recipe(cook_book_id, recipe_id)
                values( @BOOKID, @RECIPEID )
                ";

                var result = await connection.ExecuteAsync(sql,
                    new
                    {
                        BOOKID = recipeToCookBook.cook_book_id,
                        RECIPEID = recipeToCookBook.recipe_id
                    });

                return result;
            }
        }
        public List<CookBook_RecipeEntity> GetCookBookRecipesWithRecipes(int cookBookId)
        {
            return _context.CookBook_Recipe
                .Include(c => c.cook_book.account)
                .Include(c => c.recipe)
                .Include(c => c.recipe.account)
                .Where(c => c.cook_book_id == cookBookId)
                .ToList();
        }
        public async Task favor()
        {
            List<AccountEntity> allAccounts = _context.Account.ToList();

            foreach (var account in allAccounts)
            {
                var newCookBookEntry = new CookBookEntity
                {
                    name = "Yêu thích",
                    owner = account.uid
                };

                _context.Attach(newCookBookEntry);
                var entityEntry = await _context.Set<CookBookEntity>().AddAsync(newCookBookEntry);

                await _context.SaveChangesAsync();
            }

        }
    }
}
