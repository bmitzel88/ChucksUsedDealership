using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ChucksUsedDealership.Models
{
    public class ContactForm
    {
        public int ContactFormId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [MaxLength(100)]
        public required string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [MaxLength(20)]
        public required string PhoneNumber { get; set; } = "555-55-5555";

        [Required]
        [Display(Name = "Date Submitted")]
        [MaxLength(50)]
        [DefaultValue("DateTime.Now")]
        public DateTime DateSubmitted { get; set; }

        [Required]
        [Display(Name = "Subject")]
        [MaxLength(100)]
        [DefaultValue("No Subject")]
        public string SubjectLine { get; set; }

        [Required]
        [Display(Name = "Message")]
        [MaxLength(500)]
        public required string Message { get; set; }
    }

}
