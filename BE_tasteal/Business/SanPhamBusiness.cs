using AutoMapper;
using BE_tasteal.Business.Recipe;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.SamPhamTemplate;

namespace BE_tasteal.Business
{
    public class SanPhamBusiness : IRecipeBusiness<SanPhamDto, SanPhamEntity>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SanPhamBusiness> _logger;
        private readonly ISanPhamResposity _sanphamResposity;
        // private readonly IMapper _mapper;

        public SanPhamBusiness(IMapper mapper,
            ILogger<SanPhamBusiness> logger,
            ISanPhamResposity sanphamResposity)
        {
            _mapper = mapper;
            _logger = logger;
            _sanphamResposity = sanphamResposity;
        }

        #region Implement interface 
        public async Task<List<SanPhamEntity?>> GetAll()
        {
            var sanphams = await _sanphamResposity.GetAll();
            return sanphams;
        }

        public async Task<SanPhamEntity?> Add(SanPhamDto entity)
        {
            var newSanPham = _mapper.Map<SanPhamEntity>(entity);
            var sanpham = await _sanphamResposity.InsertAsync(newSanPham);

            _logger.LogInformation($"Added sanPham ", entity);
            return sanpham;
        }

        public Task<List<SanPhamEntity>?> Search(RecipeSearchDto option)
        {
            throw new NotImplementedException();
        }

        public Task<List<SanPhamEntity>?> AddFromExelAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        Task<List<RecipeEntity>> IRecipeBusiness<SanPhamDto, SanPhamEntity>.AddFromExelAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public List<RecipeEntity> GetAllRecipe()
        {
            throw new NotImplementedException();
        }

        public Task<RecipeResponse> RecipeDetail(int id)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
