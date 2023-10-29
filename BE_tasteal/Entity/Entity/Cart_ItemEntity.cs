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
        public CartEntity cart { get; set; }
        [ForeignKey("ingredient_id")]
        public IngredientEntity ingredient { get; set; }
    }
}
