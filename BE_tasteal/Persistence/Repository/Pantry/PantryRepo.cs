using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace BE_tasteal.Persistence.Repository.Pantry
{
    public class PantryRepo : GenericRepository<PantryEntity>, IPantryRepo
    {
        public PantryRepo(MyDbContext context,
         ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
        public List<RecipeEntity> FindGroupIndexContainingAnyValue(RecipesIngreAny req)
        {
            var values = req.ingredients;
            var listRecipeId = _context.Recipe_Ingredient
                                .Where(item => values.Contains(item.ingredient_id))
                                .Select(item => item.recipe_id)
                                .Distinct()
                                .ToList();

            int page = req.page.page;
            int pageSize = req.page.pageSize;

            var listRecipe = _context.Recipe
                                .Where(r => listRecipeId.Contains(r.id))
                                .Include(r => r.account)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);

            return listRecipe.ToList();
        }
        public List<RecipeEntity> FindGroupIndexContainingAllValues(RecipesIngreAny req)
        {
            //var values = req.ingredients;

            //string ingre = string.Join(",", values);
            //string count = values.Count.ToString();
            //string sql = $"SELECT recipe_id FROM Recipe_Ingredient WHERE ingredient_id IN ({ingre}) GROUP BY recipe_id HAVING COUNT(DISTINCT ingredient_id) >= {count}";
            //Console.WriteLine(sql);

            //var listRecipeId = _context.Recipe_Ingredient
            //                    .FromSqlRaw(sql)
            //                    .Select(ri => ri.recipe_id)
            //                    .ToList();
            var recipeIds = _context.Recipe_Ingredient
            .Where(ri => req.ingredients.Contains(ri.ingredient_id))
            .Select(ri => ri.recipe_id)
            .Distinct()
            .ToList();

            var groupedIngredients = _context.Recipe_Ingredient
                .Where(ri => recipeIds.Contains(ri.recipe_id))
                .GroupBy(ri => ri.recipe_id)
                .ToDictionary(g => g.Key, g => g.Select(ri => ri.ingredient_id).ToList());

            var filteredRecipeIds = groupedIngredients
                .Where(pair => req.ingredients.All(pair.Value.Contains))
                .Select(pair => pair.Key)
                .ToList();


            int page = req.page.page;
            int pageSize = req.page.pageSize;

            var listRecipe = _context.Recipe
                                .Where(r => filteredRecipeIds.Contains(r.id))
                                .Include(r => r.account)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);

            return listRecipe.ToList();
        }
        public List<RecipeEntity> FindGroupIndexContainingAnyValuesPantry(RecipesPantryAny req)
        {
            var pantry_items = _context.Pantry_Item.Where(c => c.pantry_id == req.pantry_id)
                                .Select(c => c.ingredient_id)
                                .ToList();

            var listRecipeId = _context.Recipe_Ingredient
                                .Where(item => pantry_items.Contains(item.ingredient_id))
                                .Select(item => item.recipe_id)
                                .Distinct()
                                .ToList();

            int page = req.page.page;
            int pageSize = req.page.pageSize;

            var listRecipe = _context.Recipe
                               .Where(r => listRecipeId.Contains(r.id))
                               .Include(r => r.account)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize);

            return listRecipe.ToList();
        }
        public List<RecipeEntity> FindGroupIndexContainingAllValuesPantry(RecipesPantryAny req)
        {
            var values = _context.Pantry_Item.Where(c => c.pantry_id == req.pantry_id)
                            .Select(c => c.ingredient_id)
                            .ToList();

            string ingre = string.Join(",", values);
            string count = values.Count.ToString();
            string sql = $"SELECT recipe_id FROM Recipe_Ingredient WHERE ingredient_id IN ({ingre}) GROUP BY recipe_id HAVING COUNT(DISTINCT ingredient_id) = {count}";
            Console.WriteLine(sql);

            var listRecipeId = _context.Recipe_Ingredient
                                .FromSqlRaw(sql)
                                .Select(ri => ri.recipe_id)
                                .ToList();
            //.ToList();

            int page = req.page.page;
            int pageSize = req.page.pageSize;

            var listRecipe = _context.Recipe
                                .Where(r => listRecipeId.Contains(r.id))
                                .Include(r => r.account)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);

            return listRecipe.ToList();
        }
    }
}
