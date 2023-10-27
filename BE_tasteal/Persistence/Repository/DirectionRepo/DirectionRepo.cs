using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;

namespace BE_tasteal.Persistence.Repository.Direction
{
    public class DirectionRepo: GenericRepository<Recipe_DirectionEntity>, IDirectionRepo
    {
        public DirectionRepo(MyDbContext context,
        ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }

        public IEnumerable<DirectionRes> GetDirectionByRecipeId(int id)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                SELECT ri.step, ri.image, ri.direction  from Recipe_Direction ri, recipe r
                WHERE ri.recipe_id = r.id
                AND r.id = @Id
                ";

                var result = connection.Query<DirectionRes>(sql, new { Id = id });
                return result;
            }
        }
    }
}
