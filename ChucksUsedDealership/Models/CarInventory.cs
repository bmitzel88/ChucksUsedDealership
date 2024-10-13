namespace ChucksUsedDealership.Models
{
    public class CarInventory
    {
        /// <summary>
        /// CarId is the primary key for the CarInventory table
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Make is the manufacturer of the car
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Model is the model of the car
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Year is the year the car was manufactured
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Color is the color of the car
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Mileage is the number of miles the car has been driven
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// Price is the cost of the car before taxes and fees
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Image is the URL of the image of the car
        /// </summary>
        public string Image { get; set; }
    }
}
