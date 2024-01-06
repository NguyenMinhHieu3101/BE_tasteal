using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.PantryItem
{
    public interface IPantryItemBusiness
    {
        Task<Pantry_ItemEntity> addPantryItem(CreatePantryItemReq req);
        Task<bool> removePantryItem(int req);
        Task<Pantry_ItemEntity> updatePantryItem(UpdatePantryItemReq req);
        Task<Pantry_ItemEntity> getPantryItem(int id);
        Task<List<Pantry_ItemEntity>> getAllPantryItem(GetAllPantryItemReq req);
    }
}
