using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using UseGroup.DataModel.Models;
using UserGroup.Common;
using UserGroup.Common.Contracts;
using UserGroup.Web.Models;

namespace UserGroup.Web.Pages.Person
{
    public class EditModel : PageModel
    {
        private readonly IPersonService _personService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public EditModel(IPersonService personService,
        IGroupService groupService,
            IMapper mapper)
        {
            _personService = personService ?? throw new ArgumentException(nameof(_personService));
            _groupService = groupService ?? throw new ArgumentException(nameof(_groupService));
            _mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
        }

        [BindProperty]
        public PersonViewModel Person { get; set; }

        public IEnumerable<Group> Groups { get; private set; }

        public string Heading { get; set; }

        public IActionResult OnGet(int? personId)
        {
            Heading = personId.HasValue ? Constants.Edit : Constants.Add;

            Groups = _groupService.Get();
            if (personId.HasValue)
            {
                Person = _mapper.Map<PersonViewModel>(_personService.Get(personId.Value));
            }
            else
            {
                Person = new PersonViewModel();
            }

            if (Person == null)
                return RedirectToPage("./NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Groups = _groupService.Get();
                return Page();
            }

            var personToUpsert = _mapper.Map<UseGroup.DataModel.Models.Person>(Person);

            if (Person.Id > 0)
            {
                if (!_personService.Exists(Person.Id))
                {
                    return RedirectToPage("./NotFound");
                }
                _personService.Update(personToUpsert);
            }
            else
            {
                _personService.Add(personToUpsert);
            }

            _personService.Save();

            TempData["Message"] = "Person saved!";
            return RedirectToPage("./Details", new { personId = personToUpsert.Id });
        }
    }
}