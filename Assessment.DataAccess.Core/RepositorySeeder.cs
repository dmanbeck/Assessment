using Assessment.DataAccess.Core.Entities;
using Assessment.DataAccess.Core.Interfaces;

namespace Assessment.DataAccess.Core
{
    public class RepositorySeeder
    {
        private IContactRepository _contactRepository;
        private string[] FIRST_NAMES =
        {
            "James", "Mary", "John", "Patricia", "Robert", "Jennifer", "Michael", "Linda",
            "William", "Elizabeth", "David", "Barbara", "Richard", "Susan", "Joseph", "Jessica",
            "Charles", "Sarah", "Thomas", "Karen", "Daniel", "Nancy", "Matthew", "Betty",
            "Anthony", "Helen", "Mark", "Sandra", "Donald", "Donna", "Steven", "Carol",
            "Paul", "Ruth", "Andrew", "Sharon", "Joshua", "Michelle", "Kenneth", "Laura",
            "Kevin", "Lisa", "Brian", "Nancy", "George", "Kimberly", "Edward", "Deborah",
            "Ronald", "Maria", "Timothy", "Emily"
        };

        private string[] LAST_NAMES =
        {
            "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson",
            "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin",
            "Thompson", "Garcia", "Martinez", "Robinson", "Clark", "Rodriguez", "Lewis", "Lee",
            "Walker", "Hall", "Allen", "Young", "King", "Wright", "Scott", "Torres", "Nguyen",
            "Hill", "Adams", "Baker", "Nelson", "Carter", "Mitchell", "Perez", "Roberts", "Evans",
            "Turner", "Diaz", "Cruz", "Sanchez", "Morris", "Price", "Bennett", "Wood", "Barnes",
            "Ross", "Henderson", "Coleman", "Jenkins", "Perry", "Powell", "Long", "Patterson"
        };

        public RepositorySeeder(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void Seed()
        {
            var random = new Random();

            var contactCount = random.Next(200) + 1;

            for (int i = 0; i < contactCount; i++)
            {
                var firstName = FIRST_NAMES[random.Next(50)];
                var lastName = LAST_NAMES[random.Next(50)];
                var emailCount = random.Next(3) + 1;

                var emails = new List<Email>();
                for (int j = 0; j < emailCount; j++)
                {
                    emails.Add(new Email
                    {
                        Address = $"{firstName[0]}{lastName}+{j + 1}@seedemail.com".ToLower(),
                        IsPrimary = j == 0,
                    });
                }

                _contactRepository.CreateContact(new Contact
                {
                    Name = $"{firstName} {lastName}",
                    Birthdate = new DateOnly(random.Next(100) + 1924, 1, 1).AddDays(random.Next(365)),
                    Emails = emails,
                });
                _contactRepository.Save();
            }
        }
    }
}
