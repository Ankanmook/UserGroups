using System;
using System.ComponentModel.DataAnnotations;

namespace UserGroup.Common.DTO
{
    public class PersonDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name must be provided")]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public string GroupName { get; set; }

        [Required]
        public int GroupId { get; set; }
    }


    ///can use fluent validation
    public class PersonCreationDto
    {

        [Required(ErrorMessage = "Name must be provided")]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public int GroupId { get; set; }
    }

    public class PersonUpdateDto
    {
        [Required(ErrorMessage = "Name must be provided")]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]//debatabled
        public DateTime DateAdded { get; set; }
        [Required]
        public int GroupId { get; set; }

    }
}