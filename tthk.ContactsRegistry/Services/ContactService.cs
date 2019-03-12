using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tthk.ContactsRegistry.Data;
using tthk.ContactsRegistry.Services.Interfaces;

namespace tthk.ContactsRegistry.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactsContext _context;

        public ContactService(ContactsContext context)
        {
            this._context = context;
        }

        public Contact Create(Contact contact)
        {
            var result = _context.Contacts.Add(contact);
            _context.SaveChanges();
            return result.Entity;
        }

        public async Task Delete(Guid id)
        {
          var contact = await _context.Contacts.FindAsync(id);
          await Delete(contact);
        }

        public async Task Delete(Contact contact)
        {
          _context.Remove(contact);
          await _context.SaveChangesAsync();
        }


        public async Task<Contact> Get(string id)
        {
            try
            {
                return await _context.Contacts.FindAsync(new Guid(id));
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<Contact> Get()
        {
            return _context.Contacts.ToList();
        }

    
        public async Task<Contact> Update(Guid id, Contact contact)
        {
              var contactU = _context.Contacts.Update(contact);
              await _context.SaveChangesAsync();
              return contactU.Entity;
        }

    }
}
