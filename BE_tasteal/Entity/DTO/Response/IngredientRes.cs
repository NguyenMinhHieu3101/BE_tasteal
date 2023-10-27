namespace BE_tasteal.Entity.DTO.Response
{
    public class IngredientRes
    {
        public string name { get; set; }
        public string? image { get; set; }
        public int amount { get; set; }
        public bool isLiquid { get; set; }
    }
}
