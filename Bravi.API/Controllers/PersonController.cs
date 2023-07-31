using Bravi.Application.Person.Inputs;
using Bravi.Application.Person.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bravi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAll([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var result = await _personService.FindAll(pageIndex, pageSize, null);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _personService.FindById(id);
            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateNewPerson person)
        {
            var result = await _personService.Update(person);
            if (result)
                return NoContent();


            return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _personService.Delete(id);
            if (result)
                return NoContent();


            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterNewPersonInput input)
        {
            var result = await _personService.Add(input);
            return Created(string.Empty, new {id = result });
        }

        [HttpPost("{id}/contacts")]
        public async Task<IActionResult> AddContact(Guid id,  [FromBody] RegisterNewContactInput contact)
        {
            var result = await _personService.AddContract(contact, id);
            if (result == null)
                return BadRequest();
            

            return Created(string.Empty, new {id = result } );
        }

        [HttpDelete("contacts/{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var result = await _personService.DeleteContact(id);
            if (result == false)
            {
                return BadRequest();
            }

            return NoContent();
        }


    }
}