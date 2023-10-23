using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.OccasionRepo;
using BE_tasteal.Persistence.Repository.RecipeRepo;

namespace BE_tasteal.Business.HomeBusiness
{
    public class HomeBusiness : IHomeBusiness
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IAuthorRepo _authorRepo;
        private readonly IOccasionRepo _occasionRepo;
        public HomeBusiness(
            IRecipeRepository recipeRepository,
            IAuthorRepo authorRepo,
            IOccasionRepo occasionRepo)
        {
            _recipeRepository = recipeRepository;
            _authorRepo = authorRepo;
            _occasionRepo = occasionRepo;
        }
        public Task<List<AccountEntity>> GetAuthor()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OccasionEntity> GetAllOccasion()
        {
            return _occasionRepo.GetAll();
        }

        public Task<List<RecipeEntity>> GetRecipeByRating()
        {
            throw new NotImplementedException();
        }

        public Task<List<RecipeEntity>> GetRecipeByTime()
        {
            throw new NotImplementedException();
        }
    }
}
