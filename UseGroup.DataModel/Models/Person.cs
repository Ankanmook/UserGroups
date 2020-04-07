using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UseGroup.DataModel.Models
{
    public partial class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public int GroupId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateAdded { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("Person")]
        public virtual Group Group { get; set; }
    }
}
