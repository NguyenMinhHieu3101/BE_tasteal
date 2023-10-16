using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Rating")]
    public class RatingEntity
    {
        public int recipe_id { get; set; }
        public int account_id { get; set; }
        public int rating { get; set; }
        [ForeignKey("account_id")]
        public AccountEntity? account { get; set; }
        [ForeignKey("recipe_id")]
        public RecipeEntity? recipe { get; set; }
    }
}

