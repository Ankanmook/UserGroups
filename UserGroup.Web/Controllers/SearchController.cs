using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UserGroup.Common.Helper;
using UserGroup.Web.Models;
using UserGroup.Common.Contracts;

namespace UserGroup.Web.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : Controller
    {
        private ILogger<SearchController> _logger;
        private ISearchService _searchService;
        private readonly IMapper _mapper;

        public SearchController(ILogger<SearchController> logger,
            ISearchService searchService,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _searchService = searchService ?? throw new ArgumentException(nameof(searchService));
            _mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchResourceParameter resourceParameter)
        {
            try
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                var searchResult = await _searchService.Get(resourceParameter);
                timer.Stop();

                var searchResponse = new SearchViewModel()
                {
                    Total = searchResult.Any() ? searchResult.Count : 0,
                    Results = _mapper.Map<IEnumerable<SearchResultViewModel>>(searchResult),
                    SearchOption = resourceParameter.SearchOption,
                    ResponseTime = timer.ElapsedMilliseconds
                };
                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception happened searching person", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }
    }
}