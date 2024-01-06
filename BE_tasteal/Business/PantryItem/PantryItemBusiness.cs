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
        public async Task<Pantry_ItemEntity> addPantryItem(CreatePantryItemReq req)
        {
            return await _pantryItemRepo.addPantryItem(req);
        }

        public async Task<bool> removePantryItem(int req)
        {
            return await _pantryItemRepo.removePantryItem(req);
        }
        public async Task<Pantry_ItemEntity> updatePantryItem(UpdatePantryItemReq req)
        {
            return await _pantryItemRepo.updatePantryItem(req);
        }

        public async Task<Pantry_ItemEntity> getPantryItem(int id)
        {
            return await _pantryItemRepo.getPantryItem(id);
        }
        public async Task<List<Pantry_ItemEntity>> getAllPantryItem(GetAllPantryItemReq req)
        {
            return await _pantryItemRepo.getAllPantryItem(req);
        }
    }
}
