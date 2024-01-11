using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Comment")]
    public class CommentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int recipe_id { get; set; }
        public string account_id { get; set; }
        [Column(TypeName = "text")]
        public string? comment { get; set; }
        public string? image { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        [DefaultValue(false)]
        public bool isDeleted { get; set; }

        [ForeignKey("recipe_id")]
        public RecipeEntity? Recipe { get; set; }
        [ForeignKey("account_id")]
        public AccountEntity? Account { get; set; }
    }
}
