using Assessment.DataAccess.Core.Entities;

namespace Assessment.DataAccess.Core.Interfaces
{
    public interface IContactRepository
    {
        public IEnumerable<Contact> GetContacts(string? name = null, DateOnly? startBirthdate = null, DateOnly? endBirthdate = null);
        public Contact CreateContact(Contact contact);
        public Contact GetContact(long id);
        public Contact UpdateContact(Contact contact);
        public Contact DeleteContact(long id);

        public IEnumerable<Email> GetEmails();
        public Email CreateEmail(Email email);
        public Email GetEmail(long id);
        public Email UpdateEmail(Email email);
        public Email DeleteEmail(long id);
        public IEnumerable<Email> GetContactEmails(long contactId);

        public void Save();
    }
}
