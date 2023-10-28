using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Cart")]
    public class CartEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int accountId { get; set; }
        public int recipeId { get; set; }
        public bool isBought { get; set; }
        [ForeignKey("accountId")]
        public AccountEntity account { get; set; }
        [ForeignKey("recipeId")]
        public RecipeEntity recipe { get; set; }
    }
}
