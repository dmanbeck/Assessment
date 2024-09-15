using Assessment.Core.Interfaces;
using Assessment.DataAccess.Core;
using Assessment.DataAccess.Core.Interfaces;
using Assessment.DataAccess.EntityFramework;
using Assessment.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ContactDbContext>(opt => opt.UseInMemoryDatabase("Contact"));
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

using IHost host = builder.Build();

Run(host.Services);

static void Run(IServiceProvider hostProvider)
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    var provider = serviceScope.ServiceProvider;

    var repository = provider.GetRequiredService<IContactRepository>();
    var seeder = new RepositorySeeder(repository);
    seeder.Seed();

    var contactService = provider.GetRequiredService<IContactService>();

    //var contact = contactService.CreateContact(new ContactInfo { Name = "Dan", Birthdate = new DateOnly(1989, 5, 20), Emails = new List<EmailInfo> { new EmailInfo { Address = "dmanbeck@asicentral.com", IsPrimary = true } } });

    //var serviceContact = contactService.GetContact(contact.Id);

    var contacts = contactService.SearchContacts();
}
