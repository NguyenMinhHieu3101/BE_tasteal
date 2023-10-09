﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Ingredient_Type")]
    public class Ingredient_TypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ingredient_type_id { get; set; }
        [Required]
        [MaxLength(255)]
        public required string name { get; set; }
        public int measurement_unit_id { get; set; }
    }
}