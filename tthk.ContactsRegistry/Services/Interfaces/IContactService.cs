using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tthk.ContactsRegistry.Data;

namespace tthk.ContactsRegistry.Services.Interfaces
{
    public interface IContactService
    {
        List<Contact> Get();

        Task<Contact> Get(string id);

        Contact Create(Contact contact);

        Task<Contact> Update(Guid id, Contact contact);

        Task Delete(Guid id);
    }
}
