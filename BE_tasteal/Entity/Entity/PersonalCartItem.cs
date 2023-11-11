using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.Entity
{
    public class PersonalCartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int ingredient_id { get; set; }
        public string account_id { get; set; }
        public int amount { get; set; }
        public bool is_bought { get; set; }
        [ForeignKey("ingredient_id")]
        public IngredientEntity ingredient { get; set; }
        [ForeignKey("account_id")]
        public AccountEntity account { get; set; }  
    }
}
