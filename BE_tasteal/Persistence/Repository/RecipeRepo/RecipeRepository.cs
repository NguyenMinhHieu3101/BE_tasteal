using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Interface.RecipeRepo;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.RecipeRepo
{
    public class RecipeRepository : GenericRepository<RecipeEntity>, IRecipeRepository
    {
        public RecipeRepository(MyDbContext context,
           ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }

        public async Task InsertRecipeIngredient(RecipeEntity recipe, List<IngredientEntity> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                Recipe_IngredientEntity newRecipeIngre = new Recipe_IngredientEntity
                {
                    recipe_id = recipe.id,
                    ingredient_id = ingredient.id,
                    amount = ingredient.amount
                };
                _context.Attach(newRecipeIngre);
                await _context.Set<Recipe_IngredientEntity>().AddAsync(newRecipeIngre);
                await _context.SaveChangesAsync();

            }
        }
        public async Task InsertDirection(RecipeEntity recipe, List<RecipeDirectionDto> directions)
        {
            foreach (var direction in directions)
            {
                Recipe_DirectionEntity newDirection = new Recipe_DirectionEntity
                {
                    recipe_id = recipe.id,
                    step = direction.step,
                    direction = direction.direction,
                    image = direction.image,
                };
                _context.Attach(newDirection);
                await _context.Set<Recipe_DirectionEntity>().AddAsync(newDirection);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateNutrition(RecipeEntity recipe, List<IngredientEntity> ingredients)
        {
            Nutrition_InfoEntity newNutri = new Nutrition_InfoEntity
            {
                calories = 0,
                fat = 0,
                saturated_fat = 0,
                trans_fat = 0,
                cholesterol = 0,
                carbohydrates = 0,
                fiber = 0,
                sugars = 0,
                protein = 0,
                sodium = 0,
                vitaminD = 0,
                calcium = 0,
                iron = 0,
                potassium = 0,
            };
            foreach (var ingre in ingredients)
            {
                var nutritionOfIngre = ingre.nutrition_info;

                newNutri.calories += nutritionOfIngre.calories * ingre.amount / 100;
                newNutri.fat += nutritionOfIngre.fat * ingre.amount / 100;
                newNutri.saturated_fat += nutritionOfIngre.saturated_fat * ingre.amount / 100;
                newNutri.trans_fat += nutritionOfIngre.trans_fat * ingre.amount / 100;
                newNutri.cholesterol += nutritionOfIngre.cholesterol * ingre.amount / 100;
                newNutri.carbohydrates += nutritionOfIngre.carbohydrates * ingre.amount / 100;
                newNutri.fiber += nutritionOfIngre.fiber * ingre.amount / 100;
                newNutri.sugars += nutritionOfIngre.sugars * ingre.amount / 100;
                newNutri.protein += nutritionOfIngre.protein * ingre.amount / 100;
                newNutri.sodium += nutritionOfIngre.sodium * ingre.amount / 100;
                newNutri.vitaminD += nutritionOfIngre.vitaminD * ingre.amount / 100;
                newNutri.calcium += nutritionOfIngre.calcium * ingre.amount / 100;
                newNutri.iron += nutritionOfIngre.iron * ingre.amount / 100;
                newNutri.potassium += nutritionOfIngre.potassium * ingre.amount / 100;
            }


            _context.Attach(newNutri);
            var nutriAdded = await _context.Set<Nutrition_InfoEntity>().AddAsync(newNutri);
            await _context.SaveChangesAsync();

            recipe.nutrition_info_id = nutriAdded.Entity.id;
            _context.Attach(newNutri);
            await _context.SaveChangesAsync();
        }
    }
}
