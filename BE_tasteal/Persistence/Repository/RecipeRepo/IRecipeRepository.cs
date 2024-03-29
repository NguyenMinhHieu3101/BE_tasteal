﻿using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.RecipeRepo
{
    public interface IRecipeRepository : IGenericRepository<RecipeEntity>
    {
        Task InsertRecipeIngredient(RecipeEntity recipe, List<IngredientEntity> ingredients);
        Task InsertDirection(RecipeEntity recipe, List<RecipeDirectionReq> direction);
        Task UpdateNutrition(RecipeEntity recipe, List<IngredientEntity> ingredients);
        List<RecipeEntity> GetRecipesWithIngredientsAndNutrition();
        IEnumerable<RecipeEntity> RecipeByRating(PageFilter filter);
        IEnumerable<RecipeEntity> RecipeByTime(PageFilter filter);
        IEnumerable<RelatedRecipeRes> GetRelatedRecipeByAuthor(string id);
        List<int> GetAllRecipeId(PageReq req);
        Task<List<KeyWordRes>> GetKeyWords();
        Task<int> DeleteRecipeAsync(int id);
        Task<List<RecipeEntity>> GetAll(PageReq req);
        List<RecipeEntity> getRecommendRecipesByIngredientIds(List<int> ingredientIds, PageReq _page);
        (List<RecipeEntity>, int) GetRecipesByUserId(RecipeByUids req);
        RecipeEntity? FindByIdAsyncWithNutrition(int id);
        RecipeEntity? closetRecipeDiff(decimal dis1);
    }
}
