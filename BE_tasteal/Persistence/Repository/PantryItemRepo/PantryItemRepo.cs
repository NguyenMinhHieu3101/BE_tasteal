using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Migrations;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BE_tasteal.Persistence.Repository.PantryItemRepo
{
    public class PantryItemRepo : GenericRepository<Pantry_ItemEntity> , IPantryItemRepo
    {
        public PantryItemRepo(MyDbContext context,
        ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
        public async Task<bool> addPantryItem(PantryItemReq req)
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
                  
                

                try
                {
                     query = "INSERT INTO pantry_item (pantry_id, ingredient_id, amount) VALUES (@pantry_id, @ingredient_id, @amount) " +
               "ON DUPLICATE KEY UPDATE amount = amount + @amount";
                    await connection.ExecuteAsync(query, new
                    {
                        pantry_id = item.id,
                        ingredient_id = req.ingredient_id,
                        amount = req.number
                    });
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<bool> removePantryItem (PantryItemReq req)
        {
            using (var connection = _connection.GetConnection())
            {
                var pantry_item = new Pantry_ItemEntity
                {
                    ingredient_id = req.ingredient_id,
                };

                var query = "select * from pantry where account_id = @account_id";
                var result = await connection.QueryFirstOrDefaultAsync<PantryEntity>(query, new
                {
                    account_id = req.account_id
                });

                pantry_item.pantry_id = result.id;

                query = "select * from pantry_item where pantry_id = @pantry_id and ingredient_id = @ingredient_id";
                var result1 = await connection.QueryFirstOrDefaultAsync<Pantry_ItemEntity>(query, new
                {
                    pantry_id = pantry_item.pantry_id,
                    ingredient_id = req.ingredient_id
                });
                
                if (result1 == null)
                {
                    return false;
                }

                pantry_item.amount = result1.amount;

                if (pantry_item.amount <= req.number)
                {
                    query = "delete from pantry_item where pantry_id = @pantry_id and ingredient_id = @ingredient_id";
                    _ = await connection.ExecuteAsync(query, new
                    {
                        pantry_id = pantry_item.pantry_id,
                        ingredient_id = pantry_item.ingredient_id
                    });
                }
                else
                {
                    query = "update pantry_item set amount = amount - @amount where pantry_id = @pantry_id and ingredient_id = @ingredient_id";
                    _ = await connection.ExecuteAsync(query, new
                    {
                        amount = req.number,
                        pantry_id = pantry_item.pantry_id,
                        ingredient_id = pantry_item.ingredient_id
                    });
                }
                return true;
            }

        }
    }
}
