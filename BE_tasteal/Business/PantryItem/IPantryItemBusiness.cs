using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.PantryItem
{
    public interface IPantryItemBusiness
    {
        Task<Pantry_ItemEntity> addPantryItem(PantryItemReq req);
        Task<bool> removePantryItem(int req);
    }
}
