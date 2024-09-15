using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.DataAccess.Core.Entities
{
    public class Email
    {
        [Key]
        public long Id { get; set; }
        public long ContactId { get; set; }
        public bool IsPrimary { get; set; }
        public string Address { get; set; }

        [ForeignKey(nameof(ContactId))]
        public virtual Contact Contact { get; set; }
    }
}
