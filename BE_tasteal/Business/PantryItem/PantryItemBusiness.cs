using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.PantryItemRepo;

namespace BE_tasteal.Business.PantryItem
{
    public class PantryItemBusiness : IPantryItemBusiness
    {
        private IPantryItemRepo _pantryItemRepo;
        public PantryItemBusiness(IPantryItemRepo pantryItemRepo)
        {
            _pantryItemRepo = pantryItemRepo;
        }
        public async Task<bool> addPantryItem(PantryItemReq req)
        {
            return await _pantryItemRepo.addPantryItem(req);
        }

        public async Task<bool> removePantryItem(PantryItemReq req)
        {
            return await _pantryItemRepo.removePantryItem(req);
        }
    }
}
