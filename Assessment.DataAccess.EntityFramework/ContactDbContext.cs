using Assessment.DataAccess.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment.DataAccess.EntityFramework
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}
