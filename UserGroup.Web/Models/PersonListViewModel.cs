using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroup.Common.DTO;

namespace UserGroup.Web.Models
{
    public class PersonListViewModel
    {

        public IEnumerable<PersonDto> Persons { get; set; }
        public IEnumerable<GroupDto> Groups { get; set; }
        public string SearchTerm { get; set; }
        
    }

    public class PersonViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name must be provided")]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]//debatabled
        public DateTime DateAdded { get; set; }
        [Required]
        public int GroupId { get; set; }

    }
}
