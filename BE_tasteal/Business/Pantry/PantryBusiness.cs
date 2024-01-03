using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.Pantry;

namespace BE_tasteal.Business.Pantry
{
    public class PantryBusiness
    {
        private readonly IPantryRepo _pantryRepo;

        public PantryBusiness(IPantryRepo pantryBusiness)
        {
            _pantryRepo = pantryBusiness;
        }

        public List<RecipeEntity> FindGroupIndexContainingAnyValue(RecipesIngreAny req)
        {
            return _pantryRepo.FindGroupIndexContainingAnyValue(req);
        }

        public List<RecipeEntity> FindGroupIndexContainingAllValues(RecipesIngreAny req)
        {
            return _pantryRepo.FindGroupIndexContainingAllValues(req);
        }
        public List<RecipeEntity> FindGroupIndexContainingAnyValuesPantry(RecipesPantryAny req)
        {
            return _pantryRepo.FindGroupIndexContainingAnyValuesPantry(req);
        }
        public List<RecipeEntity> FindGroupIndexContainingAllValuesPantry(RecipesPantryAny req)
        {
            return _pantryRepo.FindGroupIndexContainingAnyValuesPantry(req);
        }
    }
}
