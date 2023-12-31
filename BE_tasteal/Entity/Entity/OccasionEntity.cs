﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Occasion")]
    public class OccasionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column(TypeName = "text")]
        public string name { get; set; }
        [Column(TypeName = "text")]
        public string description { get; set; }
        [Column(TypeName = "text")]
        public string? image { get; set; }
        public DateTime start_at { get; set; }
        public DateTime end_at { get; set; }
        public bool  is_lunar_date { get; set; }
    }
}
