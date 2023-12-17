using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.OccasionRepo;
using BE_tasteal.Persistence.Repository.RecipeRepo;

namespace BE_tasteal.Business.HomeBusiness
{
    public class HomeBusiness : IHomeBusiness
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepo _authorRepo;
        private readonly IOccasionRepo _occasionRepo;
        private readonly ILogger<IHomeBusiness> _logger;
        public HomeBusiness(
            IRecipeRepository recipeRepository,
            IUserRepo authorRepo,
            IOccasionRepo occasionRepo,
            ILogger<IHomeBusiness> logger)
        {
            _recipeRepository = recipeRepository;
            _authorRepo = authorRepo;
            _occasionRepo = occasionRepo;
            _logger = logger;
        }
        public IEnumerable<AuthorRes> GetAuthor(PageFilter filter)
        {
            return _authorRepo.AuthorMostRecipe(filter);
        }

        public IEnumerable<OccasionEntity> GetAllOccasion()
        {
            return _occasionRepo.GetAll();
        }

        public IEnumerable<RecipeEntity> GetRecipeByRating(PageFilter filter)
        {
            return _recipeRepository.RecipeByRating(filter);
        }

        public IEnumerable<RecipeEntity> GetRecipeByTime(PageFilter filter)
        {
            return _recipeRepository.RecipeByTime(filter);
        }
    }
}
