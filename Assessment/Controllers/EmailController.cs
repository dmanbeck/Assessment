using Assessment.Core.Interfaces;
using Assessment.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IContactService _contactService;

        public EmailController(IContactService contactService) : base()
        {
            _contactService = contactService;
        }

        // GET api/<EmailController>/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<EmailInfo> Get(long id)
        {
            var email = _contactService.GetEmail(id);

            return Ok(email);
        }

        // POST api/<EmailController>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] EmailInfo value)
        {
            var email = _contactService.CreateEmail(value);

            if (email != null)
            {
                return CreatedAtAction(nameof(Post), email);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Update")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([FromBody] EmailInfo value)
        {
            var email = _contactService.UpdateEmail(value);

            if (email != null)
            {
                return Ok(email);
            }

            return BadRequest();
        }

        // DELETE api/<EmailController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(long id)
        {
            var contact = _contactService.DeleteEmail(id);

            if (contact != null)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
