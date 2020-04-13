using System;
using System.ComponentModel.DataAnnotations;

namespace UserGroup.Web.Models
{
    /// <summary>
    /// only for adding and editing person on the view model side
    /// </summary>
    public class PersonViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name must be provided")]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow; //needs to be local and then changes to utc

        [Required]
        public int GroupId { get; set; }
    }
}