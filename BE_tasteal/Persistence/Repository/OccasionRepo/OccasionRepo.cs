using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;

namespace BE_tasteal.Persistence.Repository.OccasionRepo
{
    public class OccasionRepo : GenericRepository<OccasionEntity>, IOccasionRepo
    {
        public OccasionRepo(MyDbContext context,
          ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }

        public IEnumerable<OccasionEntity> GetAll()
        {
            using (var connection = _connection.GetConnection())
            {
                var occasions = connection.Query<OccasionEntity>("SELECT * FROM Occasion");
                return occasions;
            }
        }
    }
}
