using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BE_tasteal.Persistence.Repository.IngredientTypeRepo
{
    public class IngredientTypeRepo : GenericRepository<Ingredient_TypeEntity>, IIngredientTypeRepo
    {
        private readonly ILogger<Ingredient_TypeEntity> _logger;
        public IngredientTypeRepo(MyDbContext context,
          ConnectionManager connectionManager, ILogger<Ingredient_TypeEntity> logger) : base(context, connectionManager)
        {
            _logger = logger;
        }
        public async Task<IEnumerable<Ingredient_TypeEntity>> GetAllIngredientType()
        {
            using (var connection = _connection.GetConnection())
            {
                var ingredientTypes =await connection.QueryAsync<Ingredient_TypeEntity>("SELECT * FROM ingredient_type");
                return ingredientTypes;
            }
        }
        public async Task<Ingredient_TypeEntity?> GetIngredientTypeById(int id)
        {
            using (var connection = _connection.GetConnection())
            {
                var query = "select * from ingredient_type where id = @ID";

                var result = await connection.QueryFirstOrDefaultAsync<Ingredient_TypeEntity>(query, new
                {
                    ID = id
                });
                return result;
            }
        }

        public async Task<Ingredient_TypeEntity?> CreateIngredientType(Ingredient_TypeEntity ingredientType)
        {
            using (var connection = _connection.GetConnection())
            {

                var insertQuery = "INSERT INTO ingredient_type ( name) " +
                                  "VALUES ( @name);" 
                ;

                connection.Execute(insertQuery, ingredientType);

                var createdAccount = connection.QuerySingleOrDefault<Ingredient_TypeEntity>("SELECT * FROM ingredient_type WHERE name = @name", new { ingredientType.name});

                return createdAccount;
            }
        }
        public async Task<Ingredient_TypeEntity> UpdateIngredientType(Ingredient_TypeEntity ingredientType)
        {
            using (var connection = _connection.GetConnection())
            {

                var updateQuery = "UPDATE ingredient_type SET ";

                if (!string.IsNullOrEmpty(ingredientType.name))
                {
                    updateQuery += "name = @name";
                }

                updateQuery = updateQuery+ " WHERE id = @id";

                var result = await connection.ExecuteAsync(updateQuery, ingredientType);

                return ingredientType;
            }
        }
        public async Task<Ingredient_TypeEntity?> DeleteIngredientType(int id)
        {
            using (var connection = _connection.GetConnection())
            {
                var query = "delete from ingredient_type where id = @ID";

                var result = await connection.QueryFirstOrDefaultAsync<Ingredient_TypeEntity>(query, new
                {
                    ID = id
                });
                return result;
            }
        }
    }
}
