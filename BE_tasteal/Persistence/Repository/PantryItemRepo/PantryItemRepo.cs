using BE_tasteal.API.AppSettings;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Migrations;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BE_tasteal.Persistence.Repository.PantryItemRepo
{
    public class PantryItemRepo : GenericRepository<Pantry_ItemEntity> , IPantryItemRepo
    {
        public PantryItemRepo(MyDbContext context,
        ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
        public async Task<Pantry_ItemEntity> addPantryItem(CreatePantryItemReq req)
        {
            using (var connection = _connection.GetConnection())
            {
                var item = new PantryEntity { };
 
                var query = "select * from pantry where account_id = @ID";
                var result = await connection.QueryFirstOrDefaultAsync<PantryEntity>(query, new
                {
                    ID = req.account_id
                });

                item = result; 
                
                if (result == null)
                  {
                     query = "INSERT INTO pantry (account_id) VALUES (@account_id)";
                    await connection.QueryFirstOrDefaultAsync<PantryEntity>(query, new
                    {
                        account_id = req.account_id
                    });

                    query = "select * from pantry where account_id = @ID";
                     result = await connection.QueryFirstOrDefaultAsync<PantryEntity>(query, new
                    {
                        ID = req.account_id
                    });

                    item = result;
                }
                  
                
                     query = "INSERT INTO pantry_item (pantry_id, ingredient_id, amount) VALUES (@pantry_id, @ingredient_id, @amount) " +
               "ON DUPLICATE KEY UPDATE amount = amount + @amount";
                    await connection.ExecuteAsync(query, new
                    {
                        pantry_id = item.id,
                        ingredient_id = req.ingredient_id,
                        amount = req.number
                    });
                
                Pantry_ItemEntity pantryItem = await _context.Pantry_Item.
                    Where(r => r.pantry_id == item.id && r.ingredient_id==req.ingredient_id).
                    Include(r => r.Pantry).
                    Include(r => r.Ingredient).FirstAsync();

                    
                return pantryItem;
            }
        }

        public async Task<bool> removePantryItem (int id)
        {
            using (var connection = _connection.GetConnection())
            {
                var query = "DELETE FROM pantry_item where id =@id";
                await connection.ExecuteAsync(query, new
                {
                    id= id
                });
                return true;
            }

        }
        public async Task<Pantry_ItemEntity> updatePantryItem (UpdatePantryItemReq req)
        {
            using (var connection = _connection.GetConnection())
            {
                var query = "UPDATE pantry_item SET amount=@amount where id =@id ";
                await connection.ExecuteAsync(query, new
                {
                    id = req.id,
                    amount=req.number
                });
                Pantry_ItemEntity pantryItem = await _context.Pantry_Item.
                   Where(r => r.id == req.id).
                   Include(r => r.Pantry).
                   Include(r => r.Ingredient).FirstAsync();

                return pantryItem;
            }
        }
        public async Task<Pantry_ItemEntity> getPantryItem(int id)
        {
            using (var connection = _connection.GetConnection())
            {
                Pantry_ItemEntity pantryItem = await _context.Pantry_Item.
                   Where(r => r.id == id).
                   Include(r => r.Pantry).
                   Include(r => r.Ingredient).FirstAsync();

                return pantryItem;
            }
        }
        public async Task<List<Pantry_ItemEntity>> getAllPantryItem(GetAllPantryItemReq req)
        {
            using (var connection = _connection.GetConnection())
            {
                PantryEntity pantry = await _context.Pantry.
                 Where(r => r.account_id == req.account_id).FirstAsync();

                int offset = (req.page - 1) * req.pageSize;

                IEnumerable <Pantry_ItemEntity> pantryItems = await _context.Pantry_Item.
                   Where(r => r.pantry_id == pantry.id).
                   Include(r => r.Pantry).
                   Include(r => r.Ingredient).
                   Skip(offset).
                   Take(req.pageSize).  
                   ToListAsync();

                return pantryItems.ToList();
            }
        }
    }
}
