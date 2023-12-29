using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using static Dapper.SqlMapper;


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
        public async Task<OccasionEntity> InsertAsync(OccasionReq occasionId)
        {
            OccasionEntity item = new OccasionEntity
            {
                name = occasionId.name,
                description = occasionId.description,
                image = occasionId.image,
                start_at = ConvertToDateTime(occasionId.start_at),
                end_at = ConvertToDateTime(occasionId.end_at),
                is_lunar_date = occasionId.is_lunar_date
            };

            _context.Attach(item);
            var entityEntry = await _context.Set<OccasionEntity>().AddAsync(item);

            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        private static DateTime ConvertToDateTime(string inputDateTime)
        {
            // Chuỗi ngày giờ đầu vào
            string input = "2023-12-29T08:45:30.123Z"; // Ví dụ chuỗi đầu vào

            // Định dạng của chuỗi đầu vào
            string format = "yyyy-MM-ddTHH:mm:ss.fffZ";

            // Chuyển đổi từ chuỗi ngày giờ sang kiểu DateTime
            if (DateTime.TryParseExact(input, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                throw new FormatException("Không thể chuyển đổi chuỗi ngày giờ.");
            }
        }
    }
}
