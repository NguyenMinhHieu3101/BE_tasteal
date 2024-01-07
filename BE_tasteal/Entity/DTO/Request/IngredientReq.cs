using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class IngredientReq
    {

    }
    public class IngredientPutReq
    {
        public string name { get; set; }
        public string? image { get; set; }
        public bool? isLiquid { get; set; }
        public decimal? ratio { get; set; }
        [Required]
        public Ingredient_TypeReq? ingredient_type { get; set; }
        [Required]
        public Nutrition_InfoReq? nutrition_info { get; set; }
    }
    public class Ingredient_TypeReq
    {
        public int id { get; set; }
    }

    public class Nutrition_InfoReq
    {
        public decimal? calories { get; set; }
        public decimal? fat { get; set; }
        public decimal? saturated_fat { get; set; }
        public decimal? trans_fat { get; set; }
        public decimal? cholesterol { get; set; }
        public decimal? carbohydrates { get; set; }
        public decimal? fiber { get; set; }
        public decimal? sugars { get; set; }
        public decimal? protein { get; set; }
        public decimal? sodium { get; set; }
        public decimal? vitaminD { get; set; }
        public decimal? calcium { get; set; }
        public decimal? iron { get; set; }
        public decimal? potassium { get; set; }
    }

}
