using System.ComponentModel.DataAnnotations;

namespace Assessment.Core.Models
{
    public class EmailInfo
    {
        public long Id { get; set; }
        [Required]
        public long ContactId { get; set; }
        [Required]
        public bool IsPrimary { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
