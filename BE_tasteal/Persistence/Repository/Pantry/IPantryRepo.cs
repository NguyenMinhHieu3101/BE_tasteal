using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.Pantry
{
    public interface IPantryRepo : IGenericRepository<PantryEntity>
    {
        List<RecipeEntity> FindGroupIndexContainingAnyValue(RecipesIngreAny req);
        List<RecipeEntity> FindGroupIndexContainingAllValues(RecipesIngreAny req);
        List<RecipeEntity> FindGroupIndexContainingAnyValuesPantry(RecipesPantryAny req);
        List<RecipeEntity> FindGroupIndexContainingAllValuesPantry(RecipesPantryAny req);
    }
}
