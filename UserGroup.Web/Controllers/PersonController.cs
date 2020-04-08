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
    [Route("api/person")]
    public class PersonController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(
                new List<object>
                {
                    new {id = 1, Name = "Sypderman", Group = "DC", DateAdded = DateTime.UtcNow },
                    new {id = 1, Name = "Batman", Group = "Marvel", DateAdded = DateTime.UtcNow}

                }
                );
        }

        [HttpGet("{id}")]
        public IActionResult GetPerson()
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
