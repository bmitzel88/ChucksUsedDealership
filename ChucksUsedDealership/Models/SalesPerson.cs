using System.ComponentModel.DataAnnotations;

namespace ChucksUsedDealership.Models
{
    public class SalesPerson
    {
        /// <summary>
        /// The unique identifier for the sales person
        /// </summary>
        [Key]
        public int SalesPersonId { get; set; }

        /// <summary>
        /// First name of the sales person
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the sales person
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Email address of the sales person
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Phone number of the sales person
        /// </summary>
        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
    }
}
