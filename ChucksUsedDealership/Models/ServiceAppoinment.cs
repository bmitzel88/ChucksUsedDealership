using System.ComponentModel.DataAnnotations;

namespace ChucksUsedDealership.Models
{
    public class ServiceAppoinment
    {
        /// <summary>
        /// The unique identifier for the service appointment
        /// </summary>
        [Key]
        public int ServiceAppoinmentID { get; set; }

        /// <summary>
        /// Customer ID for the service appointment
        /// </summary>
        [Required]
        public int CustomerID { get; set; }

        /// <summary>
        /// Car ID for the service appointment
        /// </summary>
        [Required]
        public int CarID { get; set; }

        /// <summary>
        /// Date of the service appointment
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100")]
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Description of the service
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ServiceDescription { get; set; }

        /// <summary>
        /// The car being serviced
        /// </summary>
        [Required]
        public CarInventory CustomerCar { get; set; }

        /// <summary>
        /// Customer who owns the car
        /// </summary>
        [Required]
        public Customer Customer { get; set; }
    }
}
