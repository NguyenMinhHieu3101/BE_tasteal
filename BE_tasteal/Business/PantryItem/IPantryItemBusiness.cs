using BE_tasteal.Entity.DTO.Request;

namespace BE_tasteal.Business.PantryItem
{
    public interface IPantryItemBusiness
    {
        Task<bool> addPantryItem(PantryItemReq req);
        Task<bool> removePantryItem(PantryItemReq req);
    }
}
