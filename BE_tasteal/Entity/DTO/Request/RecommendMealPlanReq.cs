namespace BE_tasteal.Entity.DTO.Request
{
    public class RecommendMealPlanReq
    {
        public List<int> recipe_ids { get; set; }
        public decimal weight { get; set; }
        public decimal height { get; set; }
        public int age { get; set; }
        public bool gender { get; set; }
        public decimal rate { get; set; }
        public bool? intend { get; set; }
    }
}
