using System.ComponentModel;

namespace BE_tasteal.Entity.DTO.Response
{
    public class RecommendMealPlanRes
    {
        public string state { get; set; }
        public List<planRecipe> recipe_add_ids { get; set; }
        public List<planRecipe> recipe_remove_ids { get; set; }
        public decimal standard_calories { get; set; }
        public decimal real_calories { get; set; }
    }

    public class planRecipe
    {
        public int id { get; set; }
        [DefaultValue(1)]
        public int amount { get; set; }
    }

    public enum state
    {
        equal,
        higher,
        smaller
    }
}
