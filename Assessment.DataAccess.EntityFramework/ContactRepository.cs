using Assessment.DataAccess.Core.Entities;
using Assessment.DataAccess.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assessment.DataAccess.EntityFramework
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _dbContext;

        public ContactRepository(ContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Contact CreateContact(Contact contact)
        {
            _dbContext.Contacts.Add(contact);

            return contact;
        }

        public Contact GetContact(long id)
        {
            return _dbContext.Contacts.Include(c => c.Emails).SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Contact> GetContacts(string? name = null, DateOnly? startBirthdate = null, DateOnly? endBirthdate = null)
        {
            return from c in _dbContext.Contacts.Include(c => c.Emails)
                   where c.Name.Contains(name ?? c.Name, StringComparison.InvariantCultureIgnoreCase) &&
                       c.Birthdate >= (startBirthdate ?? c.Birthdate) &&
                       c.Birthdate <= (endBirthdate ?? c.Birthdate)
                   select c;
        }

        public Contact UpdateContact(Contact contact)
        {
            var dbContact = _dbContext.Contacts.Include(c => c.Emails).SingleOrDefault(c => c.Id == contact.Id);

            if (dbContact != null)
            {
                dbContact.Name = contact.Name;
                dbContact.Birthdate = contact.Birthdate;

                foreach (var email in contact.Emails)
                {
                    var dbEmail = _dbContext.Emails.Find(email.Id);

                    if (dbEmail != null)
                    {
                        UpdateEmail(email);
                    }
                    else
                    {
                        CreateEmail(email);
                    }
                }

                foreach (var email in dbContact.Emails.Where(e => !contact.Emails.Any(m => m.Id == e.Id)))
                {
                    DeleteEmail(email.Id);
                }
            }

            return dbContact;
        }

        public Contact DeleteContact(long id)
        {
            var contact = _dbContext.Contacts.Include(c => c.Emails).SingleOrDefault(c => c.Id == id);
            if (contact != null)
            {
                _dbContext.Contacts.Remove(contact);
            }

            return contact;
        }

        public Email CreateEmail(Email email)
        {
            _dbContext.Emails.Add(email);

            return email;
        }

        public Email GetEmail(long id)
        {
            return _dbContext.Emails.Include(e => e.Contact).SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<Email> GetEmails()
        {
            return _dbContext.Emails.Include(e => e.Contact);
        }

        public Email UpdateEmail(Email email)
        {
            var dbEmail = _dbContext.Emails.Include(e => e.Contact).SingleOrDefault(e => e.Id == email.Id);

            if (dbEmail != null)
            {
                dbEmail.Address = email.Address;
                dbEmail.IsPrimary = email.IsPrimary;
            }

            return dbEmail;
        }

        public Email DeleteEmail(long id)
        {
            var email = _dbContext.Emails.Include(e => e.Contact).SingleOrDefault(c => c.Id == id);
            if (email != null)
            {
                _dbContext.Emails.Remove(email);
            }

            return email;
        }

        public IEnumerable<Email> GetContactEmails(long contactId)
        {
            return from e in _dbContext.Emails.Include(e => e.Contact)
                   where e.ContactId == contactId
                   select e;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
