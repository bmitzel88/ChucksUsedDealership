using System.ComponentModel.DataAnnotations;

namespace ChucksUsedDealership.Models
{
    public class CustomerCar
    {
        /// <summary>
        /// CustomerCarId is the unique identifier for the customer car
        /// </summary>
        [Key]
        public int CustomerCarId { get; set; }

        /// <summary>
        /// CustomerId is the unique identifier for the customer
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Make is the manufacturer of the car
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Make { get; set; }

        /// <summary>
        /// Model is the specific model of the car
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        /// <summary>
        /// Year is the year the car was manufactured
        /// </summary>
        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }

        /// <summary>
        /// VIN is the Vehicle Identification Number of the car
        /// </summary>
        [Required]
        [StringLength(17, MinimumLength = 11)]
        public int VIN { get; set; }

        /// <summary>
        /// Customer is the customer who owns the car
        /// </summary>
        public Customer Customer { get; set; }
    }
}
