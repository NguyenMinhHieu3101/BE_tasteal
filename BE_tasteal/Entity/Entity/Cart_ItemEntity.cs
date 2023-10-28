using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Cart_Item")]
    public class Cart_ItemEntity
    {
        public int cartId { get; set; }
        public int ingredient_id { get; set; }
        public int amount { get; set; }
        public bool isBought { get; set; }
        [ForeignKey("cartId")]
        public CartEntity Cart { get; set; }
        [ForeignKey("ingredient_id")]
        public IngredientEntity Ingredient { get; set; }
    }
}
