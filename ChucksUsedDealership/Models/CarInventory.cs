using System.ComponentModel.DataAnnotations;

namespace ChucksUsedDealership.Models
{
    public class CarInventory
    {
        /// <summary>
        /// CarId is the primary key for the CarInventory table
        /// </summary>
        [Key]
        public int CarId { get; set; }

        /// <summary>
        /// Make is the manufacturer of the car
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Make { get; set; }

        /// <summary>
        /// Model is the model of the car
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
        /// Color is the color of the car
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Color { get; set; }

        /// <summary>
        /// Mileage is the number of miles the car has been driven
        /// </summary>
        [Required]
        [Range(0, 1000000)]
        public int Mileage { get; set; }

        /// <summary>
        /// Price is the cost of the car before taxes and fees
        /// </summary>
        [Required]
        [Range(0, 1000000)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Image is the URL of the image of the car
        /// </summary>
        [Url]
        public string Image { get; set; }
    }
}
