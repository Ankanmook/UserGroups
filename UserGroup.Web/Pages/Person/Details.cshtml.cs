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
    public class DetailsModel : PageModel
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public DetailsModel(IPersonService personService,
            IMapper mapper)
        {
            _personService = personService ?? throw new ArgumentException(nameof(_personService));
            _mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
        }
        public PersonDto Person { get; set; }

        public IActionResult OnGet(int personId)
        {
            Person = _mapper.Map<PersonDto>(_personService.Get(personId));
            if (Person == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}