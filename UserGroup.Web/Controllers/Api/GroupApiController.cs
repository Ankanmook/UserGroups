using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using UseGroup.DataModel.Models;
using UserGroup.Common.DTO;
using UserGroup.DataModel.Helpers;
using UserGroup.Common.Contracts;

namespace UserGroup.Web.Controllers
{
    [ApiController]
    [Route("api/group")]
    public class GroupApiController : Controller
    {
        private readonly ILogger<GroupApiController> _logger;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupApiController(ILogger<GroupApiController> logger,
            IGroupService groupService,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _groupService = groupService ?? throw new ArgumentException(nameof(_groupService));
            _mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
        }

        [HttpGet]
        public IActionResult Get([FromQuery] ResourceParameters resourceParameters)
        {
            return Ok(_mapper.Map<IEnumerable<GroupDto>>(_groupService.Get(resourceParameters)));
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
        public IActionResult Post([FromBody] GroupCreationDto groupCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = _mapper.Map<Group>(groupCreationDto);

            if (!_groupService.Add(group))
            {
                return BadRequest("Group already exists");
            }

            var createdGroup = _mapper.Map<Group>(group);
            _groupService.Save();

            return CreatedAtRoute("GetGroup", new { createdGroup.Id });
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