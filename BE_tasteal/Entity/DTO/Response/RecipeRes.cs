﻿using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Entity.DTO.Response
{
    public class RecipeRes
    {
        public int id { get; set; }
        public string? name { get; set; }
        public decimal? rating { get; set; }
        public int? totalTime { get; set; }
        public int serving_size { get; set; }
        public string? introduction { get; set; }
        public string? author_note { get; set; }
        public string? image { get; set; }
        public AuthorRes author { get; set; }
        public IEnumerable<IngredientRes> ingredients { get; set; }
        public Nutrition_InfoEntity nutrition_info { get; set; }
        public IEnumerable<DirectionRes> directions { get; set; }
        public IEnumerable<CommentRes> comments { get; set; }
        public DateTime? createAt { get; set; }
        public List<OccasionEntity>? occasions { get; set; }
        public IEnumerable<RelatedRecipeRes>? relatedRecipes { get; set; }
    }
}
