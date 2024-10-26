using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ChucksUsedDealership.Models
{
    public class ContactForm
    {
        public int ContactFormId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public required string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public required string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Date Submitted")]
        public DateTime DateSubmitted { get; set; }

        [Required]
        [Display(Name = "Message")]
        [MaxLength(500)]
        public required string Message { get; set; }
    }

}
