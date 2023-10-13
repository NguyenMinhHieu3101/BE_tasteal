using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Interface.IngredientRepo;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.IngredientRepo
{
    public class IngredientRepo : GenericRepository<IngredientEntity>, IIngredientRepo
    {
        public IngredientRepo(MyDbContext context,
          ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }

        public bool IngredientTypeValid(string name)
        {
            var type = _context.ingredient_Type.FirstOrDefault(e => e.name == name);
            if (type != null)
            {
                return true;
            }
            return false;
        }
        public bool IngredientValid(string name)
        {
            var type = _context.ingredient.FirstOrDefault(e => e.name == name);
            if (type != null)
            {
                return true;
            }
            return false;
        }
        public async Task<Ingredient_TypeEntity> GetIngredientType(string name)
        {
            var type = _context.ingredient_Type.FirstOrDefault(e => e.name == name);
            if (type != null)
            {
                return type;
            }
            var ingredient_Type = new Ingredient_TypeEntity
            {
                name = name,
            };
            _context.Attach(ingredient_Type);
            var entity = await _context.Set<Ingredient_TypeEntity>().AddAsync(ingredient_Type);

            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Nutrition_InfoEntity> InsertNutrition(Nutrition_InfoEntity nutri)
        {
            _context.Attach(nutri);
            var entityEntry = await _context.Set<Nutrition_InfoEntity>().AddAsync(nutri);

            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
