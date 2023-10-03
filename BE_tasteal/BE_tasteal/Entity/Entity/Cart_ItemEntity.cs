﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Cart_Item")]
    public class Cart_ItemEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int item_id { get; set; }
        public int? account_id { get; set; }
        public int? ingredient_id { get; set; }

        public int amount { get; set; }
        [ForeignKey("account_id")]
        public AccountEntity? Account { get; set; }
        [ForeignKey("ingredient_id")]
        public IngredientEntity? Ingredient { get; set; }
    }
}
