using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserGroup.Common.Contracts;
using UserGroup.Common.DTO;

namespace UserGroup.Web.Pages.Person
{
    public class DeleteModel : PageModel
    {
       
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;


        public DeleteModel(IPersonService personService,
            IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;

        }

        public PersonDto Person { get; set; }


        public IActionResult OnGet(int personId)
        {
            Person = _mapper.Map<PersonDto>(_personService.Get(personId));
            if (Person == null)
                return RedirectToPage("./NotFound");
            
            return Page();
        }

        public IActionResult OnPost(int personId)
        {
            var person = _personService.Get(personId);

            if (person == null)
                return RedirectToPage("./NotFound");

            _personService.Delete(person);
            _personService.Save();

            TempData["Message"] = $"{person.Name} deleted";
            return RedirectToPage("./List");
        }
    }
}