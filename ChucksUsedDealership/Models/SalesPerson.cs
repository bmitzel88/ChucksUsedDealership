namespace ChucksUsedDealership.Models
{
    public class SalesPerson
    {
        /// <summary>
        /// The unique identifier for the sales person
        /// </summary>
        public int SalesPersonId { get; set; }

        /// <summary>
        /// First name of the sales person
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the sales person
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email address of the sales person
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone number of the sales person
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
