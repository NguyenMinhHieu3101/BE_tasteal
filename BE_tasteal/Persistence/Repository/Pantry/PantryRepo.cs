﻿using BE_tasteal.Entity.DTO.Request;
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
            var recipes = _context.Recipe_Ingredient
                .GroupBy(ri => ri.recipe_id)
                .ToDictionary(g => g.Key, g => g.Select(ri => ri.ingredient_id).ToList());
            var matchedRecipeIds = recipes
                .Where(pair => pair.Value.All(ingredient => req.ingredients.Contains(ingredient)))
                .Select(pair => pair.Key)
                .ToList();


            int page = req.page.page;
            int pageSize = req.page.pageSize;

            var listRecipe = _context.Recipe
                                .Where(r => matchedRecipeIds.Contains(r.id))
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
