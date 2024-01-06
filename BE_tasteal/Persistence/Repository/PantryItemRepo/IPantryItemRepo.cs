using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.PantryItemRepo
{
    public interface IPantryItemRepo : IGenericRepository<Pantry_ItemEntity>
    {
        Task<Pantry_ItemEntity> addPantryItem(CreatePantryItemReq req);
        Task<bool> removePantryItem(int id);
        Task<Pantry_ItemEntity> updatePantryItem(UpdatePantryItemReq req);
        Task<Pantry_ItemEntity> getPantryItem(int id);
        Task<List<Pantry_ItemEntity>> getAllPantryItem(GetAllPantryItemReq req);
    }
}
