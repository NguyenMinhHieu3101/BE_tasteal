using AutoMapper;
using BE_tasteal.Business.Interface;
using BE_tasteal.Entity.DTO;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Interface;

namespace BE_tasteal.Business
{
    public class RecipeBusiness : IBusiness<RecipeDto, RecipeEntity>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SanPhamBusiness> _logger;
        private readonly IRecipeRepository _recipeResposity;
        public RecipeBusiness(IMapper mapper,
           ILogger<SanPhamBusiness> logger,
           IRecipeRepository recipeResposity)
        {
            _mapper = mapper;
            _logger = logger;
            _recipeResposity = recipeResposity;
        }
        public async Task<RecipeEntity?> Add(RecipeDto entity)
        {
            var newRecipe = _mapper.Map<RecipeEntity>(entity);
            var reipce = await _recipeResposity.InsertAsync(newRecipe);

            _logger.LogInformation($"Added new recipe ", entity);
            return reipce;
        }

        public Task<List<RecipeEntity?>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
