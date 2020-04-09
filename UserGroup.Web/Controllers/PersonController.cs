using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    [Route("api/person")]
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;


        public PersonController(ILogger<PersonController> logger,
            IPersonService personService,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _personService = personService ?? throw new ArgumentException(nameof(_personService));
            _mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
        }

        [HttpGet]
        public IActionResult Get([FromQuery] ResourceParameters resourceParameters)
        {
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(_personService.Get(resourceParameters)));
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public IActionResult GetPerson(int id)
        {
            try
            {
                return Ok(_mapper.Map<PersonDto>(_personService.Get(id)));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception happened getting person with id: {id}", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonCreationDto personCreationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if group does not exist
            if (!_personService.GroupExists(personCreationDto.GroupId))
                return NotFound();

            var person = _mapper.Map<Person>(personCreationDto);
            _personService.Add(person);
            _personService.Save();

            var createdPerson = _mapper.Map<PersonDto>(person);

            return CreatedAtRoute("GetPerson", new { createdPerson.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, PersonUpdateDto personupdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if null either person or group does not exist
            if (!_personService.Exists(id) || !_personService.GroupExists(personupdateDto.GroupId))
                return NotFound();

            var person = _mapper.Map<Person>(personupdateDto);
            _personService.Update(person);
            _personService.Save();

            return NoContent();
        }

        /// <summary>
        /// Partial update only
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<PersonUpdateDto> patchDoc)
        {
            var person = _personService.Get(id); //can check exist here
            if (person == null)
                return NotFound();

            var personToPatch = _mapper.Map<PersonUpdateDto>(person);
            patchDoc.ApplyTo(personToPatch, ModelState);

            if (!_personService.GroupExists(personToPatch.GroupId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //for validating the person entity which needed patching [accidentally making the fields invalid]
            if (!TryValidateModel(personToPatch))
                return BadRequest(ModelState);

            _mapper.Map(personToPatch, person);
            _personService.Update(person);
            _personService.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _personService.Get(id);
            if (person == null)
                return NotFound();

            _personService.Delete(person);
            _personService.Save();

            return NoContent();
        }
    }
}