using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Entity.DTO.Response
{
    public class CookBook_RecipeRes
    {
        public int cook_book_id { get; set; }
        public RecipeEntity? RecipeEntity { get; set; }
    }
}
