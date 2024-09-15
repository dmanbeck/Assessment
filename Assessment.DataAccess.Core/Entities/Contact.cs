using System.ComponentModel.DataAnnotations;

namespace Assessment.DataAccess.Core.Entities
{
    public class Contact
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateOnly Birthdate { get; set; }
        
        public virtual ICollection<Email> Emails { get; set; }
    }
}
