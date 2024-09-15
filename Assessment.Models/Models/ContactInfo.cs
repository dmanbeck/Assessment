using System.ComponentModel.DataAnnotations;

namespace Assessment.Core.Models
{
    public class ContactInfo
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateOnly Birthdate { get; set; }
        public List<EmailInfo> Emails { get; set; }
    }
}
