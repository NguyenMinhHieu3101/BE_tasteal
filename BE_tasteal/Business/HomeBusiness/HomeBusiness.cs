using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.HomeBusiness
{
    public class HomeBusiness : IHomeBusiness
    {

        public HomeBusiness()
        {

        }
        public Task<List<AccountEntity>> GetAuthor()
        {
            throw new NotImplementedException();
        }

        public Task<List<OccasionEntity>> GetAllOccasion()
        {
            throw new NotImplementedException();
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
