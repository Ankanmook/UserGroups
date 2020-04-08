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
        public JsonResult Get()
        {
            return new JsonResult(
                new List<object>
                {
                    new {id = 1, Name = "Sypderman", Group = "DC", DateAdded = DateTime.UtcNow },
                    new {id = 1, Name = "Batman", Group = "Marvel", DateAdded = DateTime.UtcNow}
                }
                );
        }
    }
}
