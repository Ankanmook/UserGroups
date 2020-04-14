using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using UserGroup.Common.Contracts;
using UserGroup.Web.Models;

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

        public PersonViewModel Person { get; set; }

        public IActionResult OnGet(int personId)
        {
            Person = _mapper.Map<PersonViewModel>(_personService.Get(personId));
            if (Person == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}