using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserGroup.Services;
using UserGroup.Web.Models;

namespace UserGroup.Web.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController
    {
        private ILogger<SearchController> _logger;
        private ISearchService _searchService;
        private readonly IMapper _mapper;

        public SearchController(ILogger<SearchController> logger, 
            ISearchService searchService,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _searchService = searchService;
        }
    }
}
