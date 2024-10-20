using System.ComponentModel.DataAnnotations;

namespace ChucksUsedDealership.Models
{
    public class ContactForm
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }

}
