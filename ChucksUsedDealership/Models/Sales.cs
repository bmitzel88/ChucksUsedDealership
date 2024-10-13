using System.ComponentModel.DataAnnotations;

namespace ChucksUsedDealership.Models
{
    public class Sales
    {
        /// <summary>
        /// The unique identifier for the sales record
        /// </summary>
        [Key] public int Id { get; set; }
        public int SalesID { get; set; }

        /// <summary>
        /// The unique identifier for the car being sold
        /// </summary>
        [Required]
        public int carID { get; set; }

        /// <summary>
        /// The unique identifier for the customer buying the car
        /// </summary>
        [Required]
        public int CustomerID { get; set; }

        /// <summary>
        /// The unique identifier for the sales person selling the car
        /// </summary>
        [Required]
        public int SalesPersonID { get; set; }

        /// <summary>
        /// The date of the sale
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100")]
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// The price the car was sold for
        /// </summary>
        [Required]
        [Range(0, 1000000)]
        [DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }

        /// <summary>
        /// The car being sold
        /// </summary>
        public CarInventory Car { get; set; }

        /// <summary>
        /// Customer who bought the car
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Sales person who sold the car
        /// </summary>
        public SalesPerson SalesPerson { get; set; }
    }
}
