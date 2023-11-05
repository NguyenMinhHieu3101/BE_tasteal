namespace BE_tasteal.Entity.DTO.Response
{
    public class RelatedRecipeRes
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string totalTime { get; set; }
        public float rating { get; set; }
        public int ingredientAmount { get; set; }
        public AuthorRes author { get; set; }
    }
}
