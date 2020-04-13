﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using UserGroup.Common;
using UserGroup.Common.Contracts;
using UserGroup.Common.DTO;

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
        public PersonDto Person { get; set; }

        public IEnumerable<SelectListItem> Groups { get; private set; }

        public string Heading { get; set; }

        public IActionResult OnGet(int? personId)
        {
            Heading = personId.HasValue ? Constants.Edit : Constants.Add;

            var selectListItems = _groupService.Get().Select(s => new SelectListItem(s.Name, s.Id.ToString()));
            Groups = new SelectList(selectListItems);

            if (personId.HasValue)
            {
                Person = _mapper.Map<PersonDto>(_personService.Get(personId.Value));
            }
            else
            {
                Person = new PersonDto();
            }

            if (Person == null)
                return RedirectToPage("./NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                var selectListItems = _groupService.Get().Select(s => new SelectListItem(s.Name, s.Id.ToString()));
                Groups = new SelectList(selectListItems);
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
            return RedirectToPage("./Detail", new { personId = personToUpsert.Id });
        }
    }
}