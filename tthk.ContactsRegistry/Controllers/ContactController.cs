using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tthk.ContactsRegistry.Data;
using tthk.ContactsRegistry.Services.Interfaces;

namespace tthk.ContactsRegistry.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsContext _context;
        private IContactService _contactService;

        public ContactsController(
          ContactsContext context,
          IContactService contactService)
        {
            this._context = context;
            _contactService = contactService;
        }

        [HttpGet]
        public ActionResult<List<Contact>> Find([FromQuery] string term)
        {
            var contacts = _context.Contacts.Select(x => new { id = x.Id, name = x.Name,
            defaultPhoneNumber = x.PhoneNumbers.FirstOrDefault(pn => pn.IsDefault).Number,
            defaultEmail = x.Emails.FirstOrDefault(em => em.IsDefault).Email}).ToList();

            if(term != null){
              contacts = contacts.Where(x => x.id.ToString() == term || x.name == term || x.defaultEmail == term || x.defaultPhoneNumber == term).ToList();
            }

            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(string id)
        {
            var contact = await _contactService.Get(id);

            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost("[action]")]
        public ActionResult<Contact> Add([FromBody] Contact contact)
        {
            _contactService.Create(contact);
            return CreatedAtRoute(new { id = contact.Id.ToString() }, contact);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Update (Guid id, [FromBody] Contact contact)
        {
            var contactU = await _contactService.Update(id, contact);
            return Ok(contactU);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _contactService.Delete(id);
            return Ok(true);
        }

  }
}
