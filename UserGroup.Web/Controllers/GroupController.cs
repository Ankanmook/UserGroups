using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserGroup.Common.DTO;
using UserGroup.Services;
using UserGroup.Web.Models;


namespace UserGroup.Web.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(ILogger<GroupController> logger, 
            IGroupService groupService,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet(Name = "Get")]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<IEnumerable<GroupDto>>(_groupService.Get()));
        }

        [HttpGet("{id}", Name = "GetGroup")]
        public IActionResult GetGroup(int id)
        {
            try
            {
                return Ok(_mapper.Map<PersonDto>(_groupService.Get(id)));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception happened getting person with id: {id}", ex);
                return StatusCode(500, "A problem happened while handling your request");

            }
        }

        [HttpPost]
        public IActionResult Post(int id, [FromBody] GroupDto groupDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Put()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public IActionResult Patch()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}
