using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserGroup.Web.Models;


namespace UserGroup.Web.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(
                new List<object>
                {
                       new {id = 1, Name = "DC"},
                       new {id = 1, Name = "Marvel"}
                }
                );
        }

        [HttpGet("{id}")]
        public IActionResult GetGroup()
        {
            return new JsonResult(
                new List<object>
                {
                    new {id = 1, Name = "DC"},
                    new {id = 1, Name = "Marvel"}
                }
                );
        }

        [HttpPost]
        public IActionResult Post()
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
