using Assessment.Core.Interfaces;
using Assessment.Core.Models;
using Assessment.DataAccess.Core.Entities;
using Assessment.DataAccess.Core.Interfaces;

namespace Assessment.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public ContactInfo CreateContact(ContactInfo contact)
        {
            ArgumentNullException.ThrowIfNull(contact, nameof(contact));

            var inContact = Convert(contact);
            EnsureOnePrimaryEmail(inContact.Emails);

            var outContact = _contactRepository.CreateContact(inContact);
            _contactRepository.Save();

            return Convert(outContact);
        }

        public ContactInfo GetContact(long id)
        {
            var outContact = _contactRepository.GetContact(id);
            return Convert(outContact);
        }

        public ContactInfo UpdateContact(ContactInfo contact)
        {
            ArgumentNullException.ThrowIfNull(contact, nameof(contact));

            var inContact = Convert(contact);
            EnsureOnePrimaryEmail(inContact.Emails);

            var dbContact = _contactRepository.GetContact(contact.Id);
            if (dbContact == null)
            {
                return null;
            }

            var outContact = _contactRepository.UpdateContact(inContact);
            _contactRepository.Save();

            return Convert(outContact);
        }

        public ContactInfo DeleteContact(long id)
        {
            var outContact = _contactRepository.DeleteContact(id);
            _contactRepository.Save();
            return Convert(outContact);
        }

        public IEnumerable<ContactInfo> SearchContacts(string? name = null, DateOnly? startBirthdate = null, DateOnly? endBirthdate = null)
        {
            var outContacts = _contactRepository.GetContacts(name, startBirthdate, endBirthdate);
            return outContacts.Select(c => Convert(c));
        }

        public EmailInfo CreateEmail(EmailInfo email)
        {
            ArgumentNullException.ThrowIfNull(email);

            var contact = _contactRepository.GetContact(email.ContactId);
            if (contact == null)
            {
                return null;
            }

            var inEmail = Convert(email);

            if (inEmail.IsPrimary)
            {
                ClearPrimaryEmail(email.ContactId);
            }

            var outEmail = _contactRepository.CreateEmail(inEmail);
            _contactRepository.Save();

            return Convert(outEmail);
        }

        public EmailInfo GetEmail(long id)
        {
            var email = _contactRepository.GetEmail(id);
            return Convert(email);
        }

        public EmailInfo UpdateEmail(EmailInfo email)
        {
            ArgumentNullException.ThrowIfNull(email);

            var contact = _contactRepository.GetContact(email.ContactId);
            if (contact == null)
            {
                return null;
            }

            var inEmail = Convert(email);

            if (inEmail.IsPrimary)
            {
                var existingEmail = _contactRepository.GetEmail(inEmail.Id);
                ClearPrimaryEmail(existingEmail.ContactId);
            }

            var outEmail = _contactRepository.UpdateEmail(inEmail);
            return Convert(outEmail);
        }

        public EmailInfo DeleteEmail(long id)
        {
            var email = _contactRepository.DeleteEmail(id);

            if (email != null)
            {
                _contactRepository.Save();

                var contact = _contactRepository.GetContact(email.ContactId);

                if (contact?.Emails != null && contact.Emails.Any() && contact.Emails.Count(e => e.IsPrimary) == 0)
                {
                    var newPrimaryEmail = contact.Emails.First();
                    newPrimaryEmail.IsPrimary = true;
                    _contactRepository.UpdateEmail(newPrimaryEmail);
                    _contactRepository.Save();
                }
            }

            return Convert(email);
        }

        private void EnsureOnePrimaryEmail(IEnumerable<Email> emails)
        {
            if (emails != null && emails.Any())
            {
                var primaryEmails = emails.Where(e => e.IsPrimary).ToList();
                if (primaryEmails.Count > 1)
                {
                    foreach (var email in primaryEmails.Skip(1))
                    {
                        email.IsPrimary = false;
                    }
                }
                else if (primaryEmails.Count == 0)
                {
                    emails.First().IsPrimary = true;
                }
            }
        }

        private void ClearPrimaryEmail(long contactId)
        {
            var existingEmails = _contactRepository.GetContactEmails(contactId);
            foreach (var existingEmail in existingEmails)
            {
                existingEmail.IsPrimary = false;
                _contactRepository.UpdateEmail(existingEmail);
            }
        }

        private static ContactInfo Convert(Contact contact)
        {
            if (contact == null)
            {
                return null;
            }

            return new ContactInfo
            {
                Id = contact.Id,
                Name = contact.Name,
                Birthdate = contact.Birthdate,
                Emails = contact.Emails?.Select(e => Convert(e))?.ToList(),
            };
        }

        private static Contact Convert(ContactInfo contact)
        {
            if (contact == null)
            {
                return null;
            }

            return new Contact
            {
                Id = contact.Id,
                Name = contact.Name,
                Birthdate = contact.Birthdate,
                Emails = contact.Emails?.Select(e => Convert(e))?.ToList(),
            };
        }

        private static EmailInfo Convert(Email email)
        {
            if (email == null)
            {
                return null;
            }

            return new EmailInfo
            {
                Id = email.Id,
                ContactId = email.ContactId,
                Address = email.Address,
                IsPrimary = email.IsPrimary,
            };
        }

        private static Email Convert(EmailInfo email)
        {
            if (email == null)
            {
                return null;
            }

            return new Email
            {
                Id = email.Id,
                ContactId = email.ContactId,
                Address = email.Address,
                IsPrimary = email.IsPrimary,
            };
        }
    }
}
