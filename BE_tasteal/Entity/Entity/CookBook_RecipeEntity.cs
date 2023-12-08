using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("CookBook_Recipe")]
    public class CookBook_RecipeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int cook_book_id { get; set; }
        public int recipe_id { get; set; }
        [ForeignKey("cook_book_id")]
        public CookBookEntity? cook_book { get; set; }
        [ForeignKey("recipe_id")]
        public RecipeEntity? RecipeEntity { get; set; }
    }
}
