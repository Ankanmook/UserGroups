using System.ComponentModel.DataAnnotations;

namespace UserGroup.Common.DTO
{
    public class GroupDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name must be provided")]
        [MaxLength(128)]
        public string Name { get; set; }
    }
}