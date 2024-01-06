using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using BE_tasteal.Persistence.Repository.RecipeRepo;
using Dapper;
using static Dapper.SqlMapper;

namespace BE_tasteal.Persistence.Repository.RatingRepo
{
    public class RatingRepo : GenericRepository<RatingEntity>, IRatingRepo
    {
        private readonly IRecipeRepository _recipeRepository;
        public RatingRepo(MyDbContext context,
        ConnectionManager connectionManager,
        IRecipeRepository recipeRepository
        ) : base(context, connectionManager)
        {
            _recipeRepository = recipeRepository;
        }


        public async Task<IEnumerable<RatingRes>?> GetCommentByRecipeId(int recipe_id)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                Select rating.id as id, account.uid as account_id, rating.rating  from rating, account
                where rating.account_id = account.uid
                and rating.recipe_id = @Id
                ";

                var result = await connection.QueryAsync<RatingRes>(sql, new { Id = recipe_id });

                return result;
            }
        }
        public void CalcRating(int recipe_id)
        {
            List<RatingEntity> list = _context.Rating.Where(c => c.recipe_id == recipe_id).ToList();

            decimal numRating = 0;
            foreach (RatingEntity item in list)
            {
                numRating += item.rating;
            }
            numRating = numRating / list.Count;

            var recipe = _context.Recipe.Where(c => c.id == recipe_id).FirstOrDefault();
            recipe.rating = numRating;

            _context.Set<RecipeEntity>().Update(recipe);

            _context.SaveChanges();
        }
    }
}
