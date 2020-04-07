using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UseGroup.DataModel.Models
{
    public partial class Group
    {
        public Group()
        {
            Person = new HashSet<Person>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(128)]
        public string Name { get; set; }

        [InverseProperty("Group")]
        public virtual ICollection<Person> Person { get; set; }
    }
}
