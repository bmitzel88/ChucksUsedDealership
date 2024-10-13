namespace ChucksUsedDealership.Models
{
    public class ServiceAppoinment
    {
        /// <summary>
        /// The unique identifier for the service appointment
        /// </summary>
        public int ServiceAppoinmentID { get; set; }

        /// <summary>
        /// Customer ID for the service appointment
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Car ID for the service appointment
        /// </summary>
        public int CarID { get; set; }

        /// <summary>
        /// Date of the service appointment
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Description of the service
        /// </summary>
        public string ServiceDescription { get; set; }

        /// <summary>
        /// The car being serviced
        /// </summary>
        public CarInventory Car { get; set; }

        /// <summary>
        /// Customer who owns the car
        /// </summary>
        public Customer Customer { get; set; }
    }
}
