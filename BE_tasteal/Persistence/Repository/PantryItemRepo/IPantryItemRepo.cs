using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.PantryItemRepo
{
    public interface IPantryItemRepo : IGenericRepository<Pantry_ItemEntity>
    {
        Task<Pantry_ItemEntity> addPantryItem(PantryItemReq req);
        Task<bool> removePantryItem(int id);
    }
}
