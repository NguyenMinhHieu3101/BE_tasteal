using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Rating")]
    public class RatingEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int recipe_id { get; set; }
        public string account_id { get; set; }
        public int rating { get; set; }
        [ForeignKey("account_id")]
        public AccountEntity? account { get; set; }
        [ForeignKey("recipe_id")]
        public RecipeEntity? recipe { get; set; }
    }
}

