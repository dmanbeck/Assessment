using Assessment.Core.Interfaces;
using Assessment.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService) : base()
        {
            _contactService = contactService;
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ContactInfo> Get(long id)
        {
            var contact = _contactService.GetContact(id);

            return Ok(contact);
        }

        // POST api/<ContactController>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] ContactInfo value)
        {
            var contact = _contactService.CreateContact(value);

            if (contact != null)
            {
                return CreatedAtAction(nameof(Post), contact);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Update")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([FromBody] ContactInfo value)
        {
            var contact = _contactService.UpdateContact(value);

            if (contact != null)
            {
                return Ok(contact);
            }

            return BadRequest();
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(long id)
        {
            var contact = _contactService.DeleteContact(id);

            if (contact != null)
            {
                return NoContent();
            }

            return BadRequest();
        }

        // GET: api/<ContactController>
        [HttpGet("Search")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ContactInfo>> Search(string? name, DateOnly? startBirthdate, DateOnly? endBirthdate)
        {
            var contacts = _contactService.SearchContacts(name, startBirthdate, endBirthdate);

            return Ok(contacts);
        }
    }
}
