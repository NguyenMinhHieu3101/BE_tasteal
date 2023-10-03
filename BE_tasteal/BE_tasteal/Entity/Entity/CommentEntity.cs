using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Comment")]
    public class CommentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int comment_id { get; set; }
        public int recipe_id { get; set; }
        public int account_id { get; set; }
        [Column(TypeName = "text")]
        public string? comment { get; set; }
    }
}
