using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using UserGroup.Common.Contracts;
using UserGroup.Common.DTO;
using UserGroup.Common.Helper;
using UserGroup.DataModel.Helpers;

namespace UserGroup.Web.Pages.Person
{
    public class ListModel : PageModel
    {
        private readonly ILogger<ListModel> _logger; //see if this is needed or not
        private readonly IPersonService _personService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        

        public ListModel(ILogger<ListModel> logger,
        IPersonService personService,
        IGroupService groupService,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _personService = personService ?? throw new ArgumentException(nameof(_personService));
            _groupService = groupService ?? throw new ArgumentException(nameof(_groupService));
            _mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
        }


        public string SearchTerm { get; set; }

        

        [BindProperty(SupportsGet =true)]
        public SearchResourceParameter SearchResourceParameter { get; set; }
        public IEnumerable<PersonDto> Persons { get; private set; }
        public IEnumerable<SelectListItem> Groups { get; private set; }

        public void OnGet()
        {
            r pagePersons = _mapper.Map<IEnumerable<PersonDto>>(_personService.Get(SearchResourceParameter));
            Groups = new SelectList(_groupService.Get().Select(s=>s.Name).ToList());
        }
    }
}