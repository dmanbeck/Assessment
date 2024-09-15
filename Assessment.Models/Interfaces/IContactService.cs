using Assessment.Core.Models;

namespace Assessment.Core.Interfaces
{
    public interface IContactService
    {
        ContactInfo CreateContact(ContactInfo contact);
        ContactInfo UpdateContact(ContactInfo contact);
        ContactInfo GetContact(long id);
        ContactInfo DeleteContact(long id);
        IEnumerable<ContactInfo> SearchContacts(string? name = null, DateOnly? startBirthdate = null, DateOnly? endBirthdate = null);

        EmailInfo CreateEmail(EmailInfo email);
        EmailInfo UpdateEmail(EmailInfo email);
        EmailInfo GetEmail(long id);
        EmailInfo DeleteEmail(long id);
    }
}
