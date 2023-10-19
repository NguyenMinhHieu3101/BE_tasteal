using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Recipe")]
    public class RecipeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? name { get; set; }
        public float rating { get; set; }
        public TimeSpan? totalTime { get; set; }
        public TimeSpan? active_time { get; set; }
        public int serving_size { get; set; }
        [MaxLength(500)]
        public string? introduction { get; set; }
        [MaxLength(255)]
        public string? author_note { get; set; }
        public bool is_private { get; set; }
        [Column(TypeName = "text")]
        public string? image { get; set; }
        public int? author { get; set; }
        public int? nutrition_info_id { get; set; }
        [ForeignKey("author")]
        public AccountEntity? account { get; set; }
        [ForeignKey("nutrition_info_id")]
        public Nutrition_InfoEntity? nutrition_info { get; set; }

        [NotMapped]
        public List<IngredientEntity>? ingredients { get; set; }
    }
}
