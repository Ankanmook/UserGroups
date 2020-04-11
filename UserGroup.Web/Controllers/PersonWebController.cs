using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UseGroup.DataModel.Models;
using UserGroup.Common.Contracts;
using UserGroup.Common.DTO;
using UserGroup.DataModel.Helpers;
using UserGroup.Web.Models;
namespace UserGroup.Web.Controllers
{
    public class PersonWebController : Controller
    {
        private readonly ILogger<PersonWebController> _logger;
        private readonly IPersonService _personService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public PersonWebController(ILogger<PersonWebController> logger,
        IPersonService personService,
        IGroupService groupService,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _personService = personService ?? throw new ArgumentException(nameof(_personService));
            _groupService = groupService ?? throw new ArgumentException(nameof(_groupService));
            _mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
        }

        public IActionResult Index()
        {

            var resourceParameter = new ResourceParameters();

            var personListViewModel = new PersonListViewModel()
            {
                Persons = _mapper.Map<IEnumerable<PersonDto>>(_personService.Get(resourceParameter)),
                Groups = _mapper.Map<IEnumerable<GroupDto>>(_groupService.Get(resourceParameter))
            };

            return View(personListViewModel);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = _mapper.Map<PersonDto>(_personService.Get(id));

            if (person == null)
            {
                return NotFound();
            }
            return View(person);            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("Id,Name,DateAdded,GroupId")] PersonViewModel personViewModel)
        {
            if (id != personViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!_personService.Exists(personViewModel.Id) || !_personService.GroupExists(personViewModel.GroupId))
                    return NotFound();


                var person = _mapper.Map<Person>(personViewModel);
                _personService.Update(person);
                _personService.Save();

                RedirectToAction(nameof(Index));
            }
            return View(personViewModel);
        }

        [HttpGet]
        public ViewResult Detail()
        {
            throw new NotImplementedException();
        }

        public ViewResult Delete()
        {
            throw new NotImplementedException();
        }

    }
}


