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
  
    public class PaginationModel : PageModel
    {

        private ILogger<PaginationModel> _logger;
        private ISearchService _searchService;
        private readonly IGroupService _groupService;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IMapper _mapper;


        public PaginationModel(ILogger<PaginationModel> logger,
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
        public int CurrentPage { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public SortOrder SortOrder { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Name { get; set; } 
        [BindProperty(SupportsGet = true)]
        public string GroupName { get; set; }
        [BindProperty(SupportsGet = true)]
        public SearchOption SearchOption { get; set; }


        public int Count { get; set; }
        public int PageSize { get; set; } = 5;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public IEnumerable<SearchResultDto> Data { get; set; }
        public long ResponseTime { get; set; }
        public IEnumerable<SelectListItem> Groups { get; private set; }
        public IEnumerable<SelectListItem> SearchOptions { get; private set; }


        public async Task OnGetAsync()
        {
            //name => name
            //group => group
            
            //sort order => name group date added

            var searchResourceParameter = new SearchResourceParameter()
            {
                PageNumber = CurrentPage,
                PageSize = PageSize,
                SortColumn = SortBy,
                SortOrder = SortOrder,
                Option = SearchOption,
                Name = Name,
                Group = GroupName
            };

            Stopwatch timer = new Stopwatch();
            timer.Start();
            var searchResult = await _searchService.Get(searchResourceParameter);
            timer.Stop();

            Data = searchResult;
            Count = searchResult.Any() ? searchResult.First().TotalRows : 0;
            ResponseTime = timer.ElapsedMilliseconds;

            Groups = new SelectList(_groupService.Get().Select(s => s.Name).ToList());
            SearchOptions = _htmlHelper.GetEnumSelectList<SearchOption>();
        }
    }

}