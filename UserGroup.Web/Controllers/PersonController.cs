﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserGroup.Common.DTO;
using UserGroup.Web.Models;

namespace UserGroup.Web.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : Controller
    {
        [HttpGet (Name = "Get")]
        public IActionResult Get()
        {
            return Ok(
                new List<object>
                {
                    new {id = 1, Name = "Sypderman", Group = "DC", DateAdded = DateTime.UtcNow },
                    new {id = 1, Name = "Batman", Group = "Marvel", DateAdded = DateTime.UtcNow}

                }
                );
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public IActionResult GetPerson()
        {
            return Ok(
               new { id = 1, Name = "Sypderman", Group = "DC", DateAdded = DateTime.UtcNow }
                );
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonForCreationDto personCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if group not found 
            return NotFound();

            //request completed successfully
            return CreatedAtRoute("GetPerson", new { personCreationDto.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, PersonForUpdateDto personCreationDto)
        {
            

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            //if null either person or group
            return NotFound();

            //request completed successfully
            return NoContent();

        }

        /// <summary>
        /// Partial update only
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<PersonForUpdateDto> patchDoc)
        {
            var group = new GroupDto(); //get this from service
            var person = new PersonDto(); //get this from service

            if (group == null)
            {
                return NotFound();
            }

            if (person == null)
            {
                return NotFound();
            }

            var personToPatch = new PersonForUpdateDto()
            {

            };

            //patchDoc.ApplyTo(personToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();

        }

        [HttpDelete]
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}