namespace BE_tasteal.Entity.DTO.Response
{
    public class IngredientRes
    {
        public string Id { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public decimal? amount { get; set; }
        public decimal amount_per_serving { get; set; }
        public bool isLiquid { get; set; }
    }
}
