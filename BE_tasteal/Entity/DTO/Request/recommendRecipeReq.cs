namespace BE_tasteal.Entity.DTO.Request
{
    public class recommendRecipeReq
    {
        public List<int> IngredientIds { get; set; }
        public PageReq Page { get; set; }
    }
}
