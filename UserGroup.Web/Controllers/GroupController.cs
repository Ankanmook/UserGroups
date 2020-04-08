using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public GroupController(ILogger<GroupController> logger, IGroupService groupService)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _groupService = groupService;
        }

        [HttpGet(Name = "Get")]
        public IActionResult Get()
        {
            return Ok(
                new List<object>
                {
                       new {id = 1, Name = "DC"},
                       new {id = 1, Name = "Marvel"}
                }
                );
        }

        [HttpGet("{id}", Name = "GetGroup")]
        public IActionResult GetGroup()
        {
            return Ok(

                    new { id = 1, Name = "DC" }
                );
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
