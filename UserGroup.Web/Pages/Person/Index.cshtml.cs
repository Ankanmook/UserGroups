using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using UserGroup.Common.Contracts;
using UserGroup.Common.DTO;
using UserGroup.Common.Enums;
using UserGroup.Common.Helper;
using UserGroup.Web.Controllers;

namespace UserGroup.Web.Pages.Person
{
    public class IndexModel : PageModel
    {
        private ILogger<IndexModel> _logger;
        private ISearchService _searchService;
        private readonly IGroupService _groupService;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IMapper _mapper;

        public IndexModel(ILogger<IndexModel> logger,
            ISearchService searchService,
             IGroupService groupService,
             IHtmlHelper htmlHelper,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _searchService = searchService ?? throw new ArgumentException(nameof(searchService));
            _groupService = groupService ?? throw new ArgumentException(nameof(_groupService));
            _htmlHelper = htmlHelper;
            _mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
        }

        [BindProperty(SupportsGet = true)]
        public SearchResourceParameter SearchResourceParameter { get; set; }

        public IEnumerable<SelectListItem> Groups { get; private set; }
        public IEnumerable<SelectListItem> SearchOptions { get; private set; }

        public SearchViewModel Persons { get; set; }

        public async Task<IActionResult> OnGet()
        {
            

            try
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                var searchResult = await _searchService.Get(SearchResourceParameter);
                timer.Stop();

                Persons = new SearchViewModel()
                {
                    Total = searchResult.Any() ? searchResult.Count : 0,
                    PageNumber = SearchResourceParameter.PageNumber,
                    Results = searchResult,
                    ResponseTime = timer.ElapsedMilliseconds,
                };

                Groups = new SelectList(_groupService.Get().Select(s => s.Name).ToList());
                SearchOptions = _htmlHelper.GetEnumSelectList<SearchOption>();

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception happened searching person", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }
    }
    
    //this needs renamind
    public class SearchViewModel
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }

        public long ResponseTime { get; set; }
        public IEnumerable<SearchResultDto> Results { get; set; }

        

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < Total);
            }
        }

    }
}