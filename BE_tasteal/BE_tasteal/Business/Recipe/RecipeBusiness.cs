using AutoMapper;
using BE_tasteal.Business.Interface;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Interface.RecipeRepo;

namespace BE_tasteal.Business.Recipe
{
    public class RecipeBusiness : IBusiness<RecipeDto, RecipeEntity>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SanPhamBusiness> _logger;
        private readonly IRecipeRepository _recipeResposity;
        private readonly IRecipeSearchRepo _recipeSearchRepo;
        public RecipeBusiness(IMapper mapper,
           ILogger<SanPhamBusiness> logger,
           IRecipeRepository recipeResposity,
           IRecipeSearchRepo recipeSearchRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _recipeResposity = recipeResposity;
            _recipeSearchRepo = recipeSearchRepo;
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

        public Task<List<RecipeEntity>> Search(RecipeSearchDto option)
        {
            throw new NotImplementedException();
        }
    }
}
