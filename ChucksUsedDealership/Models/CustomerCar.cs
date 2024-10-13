namespace ChucksUsedDealership.Models
{
    public class CustomerCar
    {
        /// <summary>
        /// CustomerCarId is the unique identifier for the customer car
        /// </summary>
        public int CustomerCarId { get; set; }

        /// <summary>
        /// CustomerId is the unique identifier for the customer
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Make is the manufacturer of the car
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Model is the specific model of the car
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Year is the year the car was manufactured
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// VIN is the Vehicle Identification Number of the car
        /// </summary>
        public int VIN { get; set; }

        /// <summary>
        /// Customer is the customer who owns the car
        /// </summary>
        public Customer Customer { get; set; }
    }
}
