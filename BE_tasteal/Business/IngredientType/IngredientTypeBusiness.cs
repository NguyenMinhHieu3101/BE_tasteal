using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.IngredientTypeRepo;

namespace BE_tasteal.Business.IngredientType
{
    public class IngredientTypeBusiness : IIngredientTypeBusiness
    {
        private IIngredientTypeRepo _ingredientTypeRepo;
        public IngredientTypeBusiness(IIngredientTypeRepo ingredientTypeRepo)
        {
            _ingredientTypeRepo = ingredientTypeRepo;
        }
        public async Task<IEnumerable<Ingredient_TypeEntity>> GetAllIngredientType()
        {
            return await _ingredientTypeRepo.GetAllIngredientType();
        }
        public async Task<Ingredient_TypeEntity?> GetIngredientTypeById(DAGIngredientTypeReq ingredientType)
        {
            return await _ingredientTypeRepo.GetIngredientTypeById(ingredientType.id);
        }
        public async Task<Ingredient_TypeEntity?> CreateIngredientType(CreateIngredientTypeReq ingredientType)
        {
            Ingredient_TypeEntity item = new Ingredient_TypeEntity
            {
                name = ingredientType.name,
            };
            return await _ingredientTypeRepo.InsertAsync(item);
        }
        public async Task<Ingredient_TypeEntity> UpdateIngredientType(UpdateIngredientTypeReq ingredientType)
           {
            Ingredient_TypeEntity item = new Ingredient_TypeEntity
            {
                id = ingredientType.id,
                name = ingredientType.name,
            };
            return await _ingredientTypeRepo.UpdateIngredientType(item);
           }
        public async Task<Ingredient_TypeEntity?> DeleteIngredientType(DAGIngredientTypeReq ingredientType)
           {
            Ingredient_TypeEntity item = new Ingredient_TypeEntity
            {
                id = ingredientType.id,
            };
            var ingre = await _ingredientTypeRepo.FindByIdAsync(ingredientType.id);
            await _ingredientTypeRepo.DeleteAsync(ingre);
            return ingre;
           }
    }
}
