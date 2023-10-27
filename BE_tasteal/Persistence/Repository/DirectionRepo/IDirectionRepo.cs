using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.Direction
{
    public interface IDirectionRepo: IGenericRepository<Recipe_DirectionEntity>
    {
        IEnumerable<DirectionRes> GetDirectionByRecipeId(int id); 
    }
}
