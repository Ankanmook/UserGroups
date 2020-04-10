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
using UserGroup.Common.Enums;

namespace UserGroup.Web.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchApiController : Controller
    {
        private ILogger<SearchApiController> _logger;
        private ISearchService _searchService;
        private readonly IMapper _mapper;

        public SearchApiController(ILogger<SearchApiController> logger,
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
                    SearchOption = resourceParameter.Option,
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

        [HttpGet("options")]
        public IActionResult Options()
        {
            var searchOptions = (SearchOption[])Enum.GetValues(typeof(SearchOption));
            var searchOption = from value in searchOptions
                               select new { value = (int)value, name = value.ToString() };

            var sortOrders = (SortOrder[])Enum.GetValues(typeof(SortOrder));
            var sortOption = from value in sortOrders
                             select new { value = (int)value, name = value.ToString() };

            var searchColumns = (SearchColumn[])Enum.GetValues(typeof(SearchColumn));
            var searchColumn = from value in searchColumns
                               select new { name = value.ToString() };

            return Ok(
                new
                {
                    Description = "Options provided for searching",
                    Search = searchOption,
                    SearchColumn = searchColumn,
                    Sort = sortOption,
                });
        }
    }


}