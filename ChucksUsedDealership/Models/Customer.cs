using System.ComponentModel.DataAnnotations;

namespace ChucksUsedDealership.Models
{
    public class Customer
    {
        /// <summary>
        /// CustomerId is the primary key for the Customer table
        /// </summary>
        [Key]
        public int CustomerId { get; set; }

        /// <summary>
        /// FirstName is the first name of the customer
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName is the last name of the customer
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Address is the street address of the customer
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// Email is the email address of the customer
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// PhoneNumber is the phone number of the customer
        /// </summary>
        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// CustomerCars is a collection of cars owned by the customer
        /// </summary>
        
        public ICollection<CustomerCar> CustomerCars { get; set; }

        /// <summary>
        /// ServiceAppoinments is a collection of service appointments for the customer
        /// </summary>
        public ICollection<ServiceAppoinment> ServiceAppoinments { get; set; }
    }
}
